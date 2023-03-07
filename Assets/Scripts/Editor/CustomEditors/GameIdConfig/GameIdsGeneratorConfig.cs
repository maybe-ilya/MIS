using mis.Core;
using UnityEngine;
using UnityEditor;

namespace mis.Editor
{
    [SingletonData(
        savePath: "ProjectSettings/GameIdsGeneratorConfig.asset",
        settingsPath: "Project/Game Ids Generation")]
    public sealed class GameIdsGeneratorConfig : SavedScriptableSingleton<GameIdsGeneratorConfig>
    {
        [SerializeField]
        [CheckObject]
        private CodeGeneratorConfig _codeGenConfig;

        public CodeGeneratorConfig CodeGenConfig => _codeGenConfig;

        [SettingsProvider]
        private static SettingsProvider GetSettingsProvider() => GetSingletonSettingsProvider();
    }
}