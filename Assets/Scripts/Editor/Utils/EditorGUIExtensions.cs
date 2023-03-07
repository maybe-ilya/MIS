using UnityEditor;
using UnityEngine;
using mis.Core;

namespace mis.Editor
{
    public static class GUIExtensions
    {
        public static GUIContent ToGUI(this string text) => new(text);
        public static GUIContent ToGUI(this string text, string tooltip) => new(text, tooltip);
        public static string ToNice(this string text) => ObjectNames.NicifyVariableName(text);
        public static GUIContent ToNiceGUI(this string text) => text.ToNice().ToGUI();
    }

    public static class EditorGUIExtensions
    {
        public static void DrawPropertyFieldOnlyTooltip(this SerializedProperty property, Rect rect, bool includeChildren = true)
        {
            var tooltip = property.displayName;
            if (property.tooltip.IsFilled())
            {
                tooltip += $" - {property.tooltip}";
            }

            EditorGUI.LabelField(rect, string.Empty.ToGUI(tooltip));
            EditorGUI.PropertyField(rect, property, GUIContent.none, includeChildren);
        }
    }
}
