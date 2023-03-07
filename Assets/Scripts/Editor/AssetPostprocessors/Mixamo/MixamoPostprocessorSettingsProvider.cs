using UnityEditor;
using UnityEngine;

namespace mis.Editor
{
    public sealed class MixamoPostprocessorSettingsProvider
    {
        private const string SETTINGS_PROVIDER_PATH = "Project/Animation/Mixamo Postrpocessor";

        private static MixamoPostprocessorSettings Settings => MixamoPostprocessorSettings.instance;

        [SettingsProvider]
        public static SettingsProvider GetSettingsProvider()
        {
            Settings.hideFlags = HideFlags.None;
            var keywords = SettingsProvider.GetSearchKeywordsFromSerializedObject(new SerializedObject(Settings));
            var provider = AssetSettingsProvider.CreateProviderFromObject(SETTINGS_PROVIDER_PATH, Settings, keywords);
            provider.deactivateHandler += Settings.ApplyChanges;

            return provider;
        }
    }
}