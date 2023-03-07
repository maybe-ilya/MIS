using mis.Core;
using UnityEditor;
using UnityEngine;

namespace mis.Editor
{
    [SingletonData(
        savePath: "ProjectSettings/BootstrapGeneratorConfig.asset",
        settingsPath: "Project/Bootstrap Generator Config")]
    public sealed class BootstrapGeneratorConfig : SavedScriptableSingleton<BootstrapGeneratorConfig>
    {
        [SerializeField]
        [CheckObject]
        private CodeGeneratorConfig _servicesTypesConfig, _messageHandlersTypesConfig;

        public CodeGeneratorConfig ServicesTypesConfig => _servicesTypesConfig;

        public CodeGeneratorConfig MessageHandlersTypesConfig => _messageHandlersTypesConfig;

        [SettingsProvider]
        private static SettingsProvider GetSettingsProvider() => GetSingletonSettingsProvider();
    }
}