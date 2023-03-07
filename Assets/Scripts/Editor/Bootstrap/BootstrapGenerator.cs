using mis.Core;
using System.Linq;
using UnityEditor;

namespace mis.Editor
{
    public static class BootstrapGenerator
    {
        private static BoostrapSettings Settings => BoostrapSettings.Instance;
        private static BootstrapGeneratorConfig GeneratorConfig => BootstrapGeneratorConfig.Instance;

        private static void GenerateServicesTypes()
        {
            var servicesTypes = Settings.ServicesTypes;
            var servicesCodeGenConfig = GeneratorConfig.ServicesTypesConfig;
            CodeGenerator.Generate(servicesCodeGenConfig, servicesTypes.Select(type => new string[] { type.FullName }));
        }

        private static void GenerateMessageHandlersTypes()
        {
            var messageHandlersTypes = Settings.MessageHandlersTypes;
            var messageHandlersCodeGenConfig = GeneratorConfig.MessageHandlersTypesConfig;
            CodeGenerator.Generate(messageHandlersCodeGenConfig, messageHandlersTypes.Select(type => new string[] { type.FullName }));
        }

        [MenuItem("Tools/Generate Bootstrap")]
        public static void TryToGenerateBootstrapCode()
        {
            GenerateServicesTypes();
            GenerateMessageHandlersTypes();
        }
    }
}