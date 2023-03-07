using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;

namespace mis.Editor
{
    public class MixamoPostprocessor : AssetPostprocessor
    {
        private static MixamoPostprocessorSettings Settings => MixamoPostprocessorSettings.instance;

        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths, bool didDomainReload)
        {
            if (!Settings.Enabled
                || !importedAssets.Any()
                || movedAssets.Any()
                || movedFromAssetPaths.Any())
            {
                return;
            }

            var fbxModelPaths = importedAssets.Where(path => Path.GetExtension(path) == Settings.FileExtension);
            if (!fbxModelPaths.Any())
            {
                return;
            }

            foreach (var fbxPath in fbxModelPaths)
            {
                ProcessMixamoFBX(fbxPath);
            }
        }

        private static void ProcessMixamoFBX(string path)
        {
            var modelImporter = AssetImporter.GetAtPath(path) as ModelImporter;
            if (modelImporter == null)
            {
                return;
            }

            //  Check if already been imported
            if (modelImporter.clipAnimations.Any())
            {
                return;
            }

            var clips = modelImporter.defaultClipAnimations;
            if (clips == null || !clips.Any())
            {
                return;
            }

            var mixamoClip = clips.FirstOrDefault(clip => clip.name == Settings.DefaultClipName);
            if (mixamoClip == null)
            {
                return;
            }

            var originalPath = modelImporter.assetPath;
            var originalName = Path.GetFileNameWithoutExtension(originalPath);
            var wordsInsideName = Regex.Matches(originalName, Settings.WordMask).Select(match => match.Value).ToArray();
            var nameBuilder = new StringBuilder();

            nameBuilder.Append(Settings.NamePrefix);
            nameBuilder.Append(Settings.WordSeparator);

            var wordsCount = wordsInsideName.Length;
            for (var index = 0; index < wordsCount; ++index)
            {
                nameBuilder.Append(wordsInsideName[index]);
                if (index < wordsCount - 1)
                {
                    nameBuilder.Append(Settings.WordSeparator);
                }
            }

            var finalName = nameBuilder.ToString();

            switch (Settings.FileNameCase)
            {
                case MixamoPostprocessorSettings.StringCase.Lower:
                    finalName = finalName.ToLower();
                    break;

                case MixamoPostprocessorSettings.StringCase.Upper:
                    finalName = finalName.ToUpper();
                    break;
            }

            mixamoClip.name = finalName;
            modelImporter.clipAnimations = new[] { mixamoClip };
            modelImporter.SaveAndReimport();

            AssetDatabase.RenameAsset(originalPath, finalName);
        }
    }
}