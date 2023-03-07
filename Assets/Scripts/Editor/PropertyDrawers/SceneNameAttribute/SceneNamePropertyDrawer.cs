using UnityEngine;
using UnityEditor;
using mis.Core;

namespace mis.Editor
{
    [CustomPropertyDrawer(typeof(SceneNameAttribute))]
    public class SceneNamePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using var propertyScope = new EditorGUI.PropertyScope(position, label, property);
            var rect = EditorGUI.PrefixLabel(position, propertyScope.content);

            if (EditorGUI.DropdownButton(rect, property.stringValue.ToGUI(), FocusType.Keyboard))
            {
                PopupWindow.Show(rect, new SceneNamePopup(rect, property.Copy()));
            }
        }
    }
}