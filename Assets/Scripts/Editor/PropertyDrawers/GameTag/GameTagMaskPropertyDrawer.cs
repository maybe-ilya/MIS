using UnityEngine;
using UnityEditor;
using mis.Core;

namespace mis.Editor
{
    [CustomPropertyDrawer(typeof(GameTagMask))]
    public class GameTagMaskPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using var propertyScope = new EditorGUI.PropertyScope(position, label, property);
            var rect = EditorGUI.PrefixLabel(position, propertyScope.content);

            property.Next(true);
            var currentTagMaskName = GameTagConfig.Instance.GetNameForTagMask((GameTagMask)property.longValue);

            if (EditorGUI.DropdownButton(rect, currentTagMaskName.ToGUI(), FocusType.Passive))
            {
                PopupWindow.Show(rect, new GameTagPopup(rect, property.Copy(), canMultiSelect: true));
            }
        }
    }
}
