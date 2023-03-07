using UnityEngine;
using System.Linq;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace mis.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class PreloadedGlobalAssetAttibute : Attribute { }

    // Designed to be placed at PlayerSettings.PreloadedAssets
    // but created at ProjectSettings
    [PreloadedGlobalAssetAttibute]
    public abstract class PreloadedScriptableSignleton<T> : ScriptableObject where T : ScriptableObject
    {
        private static T _instance;

        public static T Instance => _instance ??= GetInstance();

        private static T GetInstance()
        {
#if UNITY_EDITOR
            return PlayerSettings.GetPreloadedAssets().FirstOrDefault(obj => obj is T) as T;
#else
            return Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
#endif

        }
    }
}
