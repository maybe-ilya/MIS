using System.Linq;
using UnityEngine;
using UnityEditor;

namespace mis.Editor
{
    public sealed class ForceSaveAssetsTool
    {
        private const string MENU_PATH = "Assets/Force Save Assets %#&F";

        [MenuItem(MENU_PATH, isValidateFunction: true)]
        private static bool IsAnyAssetSelected() => Selection.assetGUIDs.Any();

        [MenuItem(MENU_PATH, isValidateFunction: false)]
        private static void ForceSaveAssets()
        {
            var paths = Selection.assetGUIDs.Select(AssetDatabase.GUIDToAssetPath).ToList();

            foreach (var path in paths)
            {
                var asset = AssetDatabase.LoadAssetAtPath<Object>(path);
                EditorUtility.SetDirty(asset);
                AssetDatabase.SaveAssetIfDirty(asset);
            }

            paths.Clear();

            Resources.UnloadUnusedAssets();
            AssetDatabase.Refresh();
        }
    }
}