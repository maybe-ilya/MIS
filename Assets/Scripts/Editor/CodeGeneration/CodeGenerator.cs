using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEditor;

namespace mis.Editor
{
    public static class CodeGenerator
    {
        public static void Generate(CodeGeneratorConfig config, IEnumerable<string[]> elements)
        {
            var elementsBuilder = new StringBuilder();
            var elementFormat = config.ElementFormat;
            foreach (var elementStrings in elements)
            {
                elementsBuilder.AppendLine(string.Format(elementFormat, elementStrings));
            }

            var output = string.Format(config.MainFormat, elementsBuilder.ToString());
            File.WriteAllText(config.OutputFilePath, output);
            AssetDatabase.ImportAsset(config.OutputFilePath, ImportAssetOptions.ForceUpdate);
            AssetDatabase.Refresh();
        }
    }
}