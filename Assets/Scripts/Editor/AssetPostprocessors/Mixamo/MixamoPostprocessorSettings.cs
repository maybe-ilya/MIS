using UnityEditor;
using UnityEngine;

namespace mis.Editor
{
    [FilePath("ProjectSettings/MixamoPostprocessorSettings.asset", FilePathAttribute.Location.ProjectFolder)]
    public sealed class MixamoPostprocessorSettings : ScriptableSingleton<MixamoPostprocessorSettings>
    {
        public enum StringCase
        {
            AsIs = 0,
            Lower = 1,
            Upper = 2,
        }

        [SerializeField]
        private bool _enabled = true;
        [SerializeField]
        private string _defaultClipName = "mixamo.com";
        [SerializeField]
        private string _wordMask = @"(\w+)";
        [SerializeField]
        private string _fileExtension = ".fbx";
        [SerializeField]
        private string _namePrefix = "anim";
        [SerializeField]
        private string _wordSeparator = "_";
        [SerializeField]
        private StringCase _fileNameCase = StringCase.Lower;

        public bool Enabled => _enabled;
        public string DefaultClipName => _defaultClipName;
        public string WordMask => _wordMask;
        public string FileExtension => _fileExtension;
        public string NamePrefix => _namePrefix;
        public string WordSeparator => _wordSeparator;
        public StringCase FileNameCase => _fileNameCase;

        public void ApplyChanges() => Save(true);
    }
}