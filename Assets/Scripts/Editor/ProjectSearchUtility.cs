using System.Linq;
using UnityEditor;

namespace mis.Editor
{
	public class ProjectSearchUtility {
        private const string ScriptAssetSearchPath = "Assets/Find Assets From Script Type";
        private const string SameAssetSearchPath = "Assets/Find Assets Of Same Type";

        [MenuItem(ScriptAssetSearchPath, true)]
        private static bool AnyScriptsSelected() => Selection.assetGUIDs
          .Select(AssetDatabase.GUIDToAssetPath)
          .Select(AssetDatabase.GetMainAssetTypeAtPath)
          .Any(type => type == typeof(MonoScript));

        [MenuItem(ScriptAssetSearchPath, false)]
        private static void SearchAssetsOfSelectedScripts()
        {
            var searchTypes = Selection.assetGUIDs
              .Select(AssetDatabase.GUIDToAssetPath)
              .Where(path => AssetDatabase.GetMainAssetTypeAtPath(path) == typeof(MonoScript))
              .Select(AssetDatabase.LoadAssetAtPath<MonoScript>)
              .Select(script => script.GetClass())
              .Where(type => type != null)
              .Distinct()
              .ToArray();

            if (searchTypes.Length == 0) return;
            ProjectWindowUtility.SearchByType(searchTypes);
        }

        [MenuItem(SameAssetSearchPath, true)]
        private static bool AnyAssetSelected() => Selection.assetGUIDs
          .Select(AssetDatabase.GUIDToAssetPath)
          .Any(path => !AssetDatabase.IsValidFolder(path));

        [MenuItem(SameAssetSearchPath, false)]
        private static void SearchAssetsOfSameType()
        {
            var searchTypes = Selection.assetGUIDs
              .Select(AssetDatabase.GUIDToAssetPath)
              .Where(path => !AssetDatabase.IsValidFolder(path))
              .Select(AssetDatabase.GetMainAssetTypeAtPath)
              .Distinct()
              .ToArray();

            if (searchTypes.Length == 0) return;
            ProjectWindowUtility.SearchByType(searchTypes);
        }
    }
}