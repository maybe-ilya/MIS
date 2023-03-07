using mis.Core;
using UnityEditor;
using UnityEngine;

namespace mis.Editor
{
    [SingletonData(
        savePath: "ProjectSettings/CheckObjectAttributeSettings.asset",
        settingsPath: "Project/Check Object Attribute Settings")]
    public sealed class CheckObjectAttributeSettings : SavedScriptableSingleton<CheckObjectAttributeSettings>
    {
        [SerializeField]
        private Color _validColor = Color.green;
        [SerializeField]
        private Color _invalidColor = Color.red;

        public Color ValidColor => _validColor;
        public Color InvalidColor => _invalidColor;

        [SettingsProvider]
        private static SettingsProvider GetProvider() => GetSingletonSettingsProvider();
    }
}