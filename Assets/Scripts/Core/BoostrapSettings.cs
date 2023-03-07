#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace mis.Core
{
    [SingletonData(
        savePath: "ProjectSettings/BootsrapSettings.asset",
        settingsPath: "Project/Bootstrap Settings")]
    public class BoostrapSettings : SavedScriptableSingleton<BoostrapSettings>
    {
        [SerializeField]
        [CheckObject]
        private MonoScript[] _servicesTypes;

        [SerializeField]
        [CheckObject]
        private MonoScript[] _messageHandlersTypes;

        public Type[] ServicesTypes =>
            _servicesTypes.Select(script => script.GetClass()).ToArray();

        public Type[] MessageHandlersTypes =>
            _messageHandlersTypes.Select(script => script.GetClass()).ToArray();

        [SettingsProvider]
        private static SettingsProvider GetSettingsProvider() => GetSingletonSettingsProvider();
    }
}
#endif