using UnityEngine;
using UnityEditor;
using mis.Core;

namespace mis.Editor
{
    [CustomPropertyDrawer(typeof(GameId))]
    public class GameIdPropertyDrawer : PropertyDrawer
    {
        private static readonly GUIContent DeleteIcon = new("\u0058", "Clear");

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using var _ = new EditorGUI.PropertyScope(position, label, property);
            property.Next(true);

            var rects = EditorGUI.PrefixLabel(position, label).CutRight(18);
            var currentIdName = GameIdConfig.Instance.GetNameByValue(property.intValue);

            if (EditorGUI.DropdownButton(rects[0], currentIdName.ToGUI(), FocusType.Keyboard))
            {
                PopupWindow.Show(rects[0], new GameIdPopup(rects[0], property.Copy()));
            }

            if (GUI.Button(rects[1], DeleteIcon))
            {
                property.longValue = 0;
            }
        }
    }
}
