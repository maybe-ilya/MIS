using UnityEngine;
using UnityEditor;
using mis.Core;

namespace mis.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using var _ = new EditorGUI.PropertyScope(position, label, property);
            using var __ = new EditorGUI.DisabledScope(true);

            EditorGUI.PropertyField(position, property, label, property.hasVisibleChildren);
        }
    }
}
