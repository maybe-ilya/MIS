#if UNITY_EDITOR
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace mis.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class SingletonDataAttribute : Attribute
    {
        public readonly string SavePath;
        public readonly string SettingsPath;
        public readonly bool IsPreference;
        public readonly HideFlags HideFlags;

        public SingletonDataAttribute(
            string savePath,
            string settingsPath,
            bool isPreference = false,
            HideFlags hideFlags = HideFlags.None)
        {
            SavePath = savePath;
            SettingsPath = settingsPath;
            IsPreference = isPreference;
            HideFlags = hideFlags;
        }
    }

    public abstract class SavedScriptableSingleton<T> : ScriptableObject
        where T : ScriptableObject
    {
        private static T _instance;
        private const HideFlags DEFAULT_HIDE_FLAGS = HideFlags.None;

        public static T Instance =>
            _instance != null ? _instance : _instance = CreateOrLoadInstance();

        private static T CreateOrLoadInstance()
        {
            var singletonPath = GetSingletonPath();
            if (!singletonPath.IsFilled())
            {
                throw new SystemException($"Singleton path of {typeof(T).FullName} is empty");
            }

            if (File.Exists(singletonPath))
            {
                return InternalEditorUtility.LoadSerializedFileAndForget(singletonPath)[0] as T;
            }
            else
            {
                var newInstance = CreateInstance<T>();
                newInstance.hideFlags = GetSingletonHideFlags();
                return newInstance;
            }
        }

        protected static string GetSingletonPath()
        {
            var singletonType = typeof(T);

            if (!Attribute.IsDefined(singletonType, typeof(SingletonDataAttribute)))
            {
                throw new SystemException($"{nameof(SingletonDataAttribute)} is not defined at {singletonType.FullName} but expected");
            }

            var singletonDataAttr = singletonType.GetCustomAttribute<SingletonDataAttribute>();
            if (!singletonDataAttr.SavePath.IsFilled())
            {
                throw new SystemException($"Singleton save path of {singletonType.FullName} is empty");
            }

            return singletonDataAttr.IsPreference
                ? Path.Combine(InternalEditorUtility.unityPreferencesFolder, singletonDataAttr.SavePath)
                : singletonDataAttr.SavePath;
        }

        protected static HideFlags GetSingletonHideFlags()
        {
            var singletonType = typeof(T);

            if (!Attribute.IsDefined(singletonType, typeof(SingletonDataAttribute)))
            {
                throw new SystemException($"{nameof(SingletonDataAttribute)} is not defined at {singletonType.FullName} but expected");
            }

            return singletonType.GetCustomAttribute<SingletonDataAttribute>().HideFlags;
        }

        protected static SettingsProvider GetSingletonSettingsProvider()
        {
            var singletonType = typeof(T);

            if (!Attribute.IsDefined(singletonType, typeof(SingletonDataAttribute)))
            {
                throw new SystemException($"{nameof(SingletonDataAttribute)} is not defined at {singletonType.FullName} but expected");
            }

            var singletonDataAttr = singletonType.GetCustomAttribute<SingletonDataAttribute>();
            if (!singletonDataAttr.SettingsPath.IsFilled())
            {
                throw new SystemException($"Singleton settings path of {singletonType.FullName} is empty");
            }

            if (Instance == null)
            {
                throw new SystemException($"Instance of {singletonType.FullName} is null");
            }

            var keywords = SettingsProvider.GetSearchKeywordsFromSerializedObject(new SerializedObject(Instance));
            var settingsPath = singletonDataAttr.SettingsPath;
            //  Settings provider will be shown at ProjectSettings only. See AssetSettingsProvider constructor
            var provider = AssetSettingsProvider.CreateProviderFromObject(settingsPath, Instance, keywords);
            provider.deactivateHandler += Save;

            return provider;
        }

        protected static void Save()
        {
            if (Instance == null)
            {
                throw new NullReferenceException($"Instance of {typeof(T).FullName} is null");
            }

            var singletonPath = GetSingletonPath();
            if (!singletonPath.IsFilled())
            {
                throw new SystemException($"Singleton path of {typeof(T).FullName} is empty");
            }

            var folder = Path.GetDirectoryName(singletonPath);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            InternalEditorUtility.SaveToSerializedFileAndForget(new[] { _instance }, singletonPath, true);
        }
    }
}
#endif