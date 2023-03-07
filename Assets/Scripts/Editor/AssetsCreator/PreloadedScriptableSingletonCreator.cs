using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using mis.Core;
using UObject = UnityEngine.Object;

namespace mis.Editor
{
    public class PreloadedScriptableSingletonCreator : ScriptableWizard
    {
        private Type[] _singletonTypes;
        private GUIContent[] _signletonTypesLabels;
        private int _selectedTypeIndex;

        private void OnEnable()
        {
            var typeCollection = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => Attribute.IsDefined(type, typeof(PreloadedGlobalAssetAttibute), true))
                .Where(type => !type.IsAbstract && !type.IsGenericType);

            _singletonTypes = typeCollection.ToArray();
            _signletonTypesLabels = _singletonTypes.Select(type => new GUIContent(text: type.Name, tooltip: type.FullName)).ToArray();
        }

        protected override bool DrawWizardGUI()
        {
            var changeCheck = new EditorGUI.ChangeCheckScope();

            _selectedTypeIndex = EditorGUILayout.Popup("Selected Type".ToGUI(), _selectedTypeIndex, _signletonTypesLabels);

            return changeCheck.changed;
        }

        private void OnWizardCreate()
        {
            var selectedType = _singletonTypes[_selectedTypeIndex];
            var newInstance = CreateInstance(selectedType);
            var path = $"Assets/Content/Preloaded/{selectedType.Name}.asset";
            AssetDatabase.CreateAsset(newInstance, path);

            var preloadedAssets = PlayerSettings.GetPreloadedAssets();
            var newPreloadedAssets = preloadedAssets.Union(new UObject[] { newInstance }).ToArray();
            PlayerSettings.SetPreloadedAssets(newPreloadedAssets);
        }

        [MenuItem("Tools/Create Preloaded Scriptable Signleton")]
        private static void ShowCreator()
        {
            DisplayWizard<PreloadedScriptableSingletonCreator>("Create Preloaded Scriptable Signleton", "Create", "Cancel");
        }
    }
}
