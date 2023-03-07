using UnityEditor;
using mis.Core;
using UnityEngine;
using System.Linq;

namespace mis.Editor
{
    [CustomEditor(typeof(GameIdConfig))]
    public class GameIdConfigEditor : UnityEditor.Editor
    {
        private SerializedProperty _entriesProperty;
        private GameIdEntryTableView _entriesTableView;

        private void OnEnable()
        {
            _entriesProperty = serializedObject.FindProperty("_entries");
            _entriesTableView = new GameIdEntryTableView(_entriesProperty);
            _entriesTableView.multiColumnHeader.ResizeToFit();
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspectorGUI();
            _entriesTableView.OnGUILayout();

            using var _0 = new EditorGUI.DisabledGroupScope(!serializedObject.hasModifiedProperties);
            using var _1 = new EditorGUILayout.HorizontalScope();

            GUILayout.FlexibleSpace();

            if (GUILayout.Button(nameof(ResetChanges).ToNiceGUI()))
            {
                ResetChanges();
            }

            if (GUILayout.Button(nameof(ApplyChanges).ToNiceGUI()))
            {
                ApplyChanges();
            }
        }

        private void DrawDefaultInspectorGUI()
        {
            var iterator = serializedObject.GetIterator();
            var enterChildren = true;
            while (iterator.NextVisible(enterChildren))
            {
                using (new EditorGUI.DisabledScope("m_Script" == iterator.propertyPath))
                {
                    EditorGUILayout.PropertyField(iterator, includeChildren: true);
                }

                enterChildren = false;
            }
        }

        private void ResetChanges()
        {
            serializedObject.UpdateIfRequiredOrScript();
            _entriesTableView.Reload();
        }

        private void ApplyChanges()
        {
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssetIfDirty(target);

            var config = target as GameIdConfig;
            var codeGenConfig = GameIdsGeneratorConfig.Instance.CodeGenConfig;
            var codeGenInput = config.Entries
                .Where(entry => entry.ConstName.IsFilled())
                .Select(entry => new string[] { entry.ConstName, entry.Value.ToString(), entry.Name });

            CodeGenerator.Generate(codeGenConfig, codeGenInput);
        }
    }
}