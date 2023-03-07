using UnityEngine;

namespace mis.Editor
{
    [CreateAssetMenu(fileName = "CodeGeneratorConfig", menuName = "mis Assets/CodeGeneratorConfig")]
    public class CodeGeneratorConfig : ScriptableObject
    {
        [SerializeField]
        private string _outputFilePath;

        [SerializeField]
        [TextArea(minLines: 2, maxLines: 5)]
        private string _elementFormat;

        [SerializeField]
        [TextArea(minLines:5, maxLines: 50)]
        private string _mainFormat;

        public string OutputFilePath => _outputFilePath;
        public string ElementFormat => _elementFormat;
        public string MainFormat => _mainFormat;
    }
}