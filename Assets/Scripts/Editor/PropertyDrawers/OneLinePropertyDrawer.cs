using UnityEngine;
using UnityEditor;
using mis.Core;

namespace mis.Editor
{
    [CustomPropertyDrawer(typeof(OneLineAttribute))]
    public class OneLinePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using var propertyScope = new EditorGUI.PropertyScope(position, label, property);
            var rect = EditorGUI.PrefixLabel(position, propertyScope.content);

            var childrenCount = GetChildCount(property);
            var splitRects = rect.SplitHorizontally(childrenCount);

            var enterChildren = true;
            var currentChildIndex = 0;

            while (property.NextVisible(enterChildren)
                && currentChildIndex < childrenCount)
            {
                enterChildren = false;
                property.DrawPropertyFieldOnlyTooltip(splitRects[currentChildIndex], false);
                currentChildIndex++;
            }
        }

        private int GetChildCount(SerializedProperty property)
        {
            var result = 0;
            var current = property.Copy();
            var next = property.Copy();
            next.NextVisible(false);

            var enterChildren = true;

            while (current.NextVisible(enterChildren)
                && !SerializedProperty.EqualContents(current, next))
            {
                result++;
                enterChildren = false;
            }

            return result;
        }
    }
}