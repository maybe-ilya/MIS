using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UObject = UnityEngine.Object;

namespace mis.Editor
{
    public static class PreloadedAssetsExtensions
    {
        public static T GetAssetWithType<T>() where T : UObject
        {
            var assets = PlayerSettings.GetPreloadedAssets();
            foreach (var asset in assets)
            {
                if(asset is T typedAsset)
                {
                    return typedAsset;
                }
            }
            return null;
        }

        public static T GetAssetWithTypeAndName<T>(string name) where T : UObject
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            var assets = PlayerSettings.GetPreloadedAssets();
            foreach (var asset in assets)
            {
                if (asset is T typedAsset && typedAsset.name.Equals(name))
                {
                    return typedAsset;
                }
            }
            return null;
        }
    }
}
