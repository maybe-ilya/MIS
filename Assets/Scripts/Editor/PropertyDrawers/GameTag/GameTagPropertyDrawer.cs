using UnityEngine;
using UnityEditor;
using mis.Core;

namespace mis.Editor
{
    [CustomPropertyDrawer(typeof(GameTag))]
    public class GameTagPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using var propertyScope = new EditorGUI.PropertyScope(position, label, property);
            var rect = EditorGUI.PrefixLabel(position, propertyScope.content);

            property.Next(true);
            var currentTagName = GameTagConfig.Instance.GetNameForTag((GameTag)property.longValue);

            if (EditorGUI.DropdownButton(rect, currentTagName.ToGUI(), FocusType.Passive))
            {
                PopupWindow.Show(rect, new GameTagPopup(rect, property.Copy(), canMultiSelect: false));
            }
        }
    }
}
