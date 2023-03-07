using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace mis.Editor
{
    public sealed class BootstrapGeneratorPreprocessBuildHandler : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            BootstrapGenerator.TryToGenerateBootstrapCode();
        }
    }
}