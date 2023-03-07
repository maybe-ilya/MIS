using System;
using UnityEngine;
using UnityEditor;
using mis.Core;

namespace mis.Editor
{
    [CustomPropertyDrawer(typeof(IntRange))]
    [CustomPropertyDrawer(typeof(FloatRange))]
    [CustomPropertyDrawer(typeof(LongRange))]
    [CustomPropertyDrawer(typeof(UintRange))]
    public class RangePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using var propertyScope = new EditorGUI.PropertyScope(position, label, property);
            var rect = EditorGUI.PrefixLabel(position, propertyScope.content);
            var split = rect.SplitHorizontally(2);

            using var changeScope = new EditorGUI.ChangeCheckScope();

            property.Next(true);
            var minCopy = property.Copy();
            property.DrawPropertyFieldOnlyTooltip(split[0]);

            property.Next(false);
            var maxCopy = property.Copy();
            property.DrawPropertyFieldOnlyTooltip(split[1]);

            if (!changeScope.changed)
            {
                return;
            }

            UpdateValuesIfNeeded(minCopy, maxCopy);
        }

        private static void UpdateValuesIfNeeded(SerializedProperty min, SerializedProperty max)
        {
            switch (min.numericType)
            {
                case SerializedPropertyNumericType.Int32:
                    min.intValue = Math.Min(min.intValue, max.intValue);
                    break;

                case SerializedPropertyNumericType.UInt32:
                    min.uintValue = Math.Min(min.uintValue, max.uintValue);
                    break;

                case SerializedPropertyNumericType.Int64:
                    min.longValue = Math.Min(min.longValue, max.longValue);
                    break;

                case SerializedPropertyNumericType.Float:
                    min.floatValue = Math.Min(min.floatValue, max.floatValue);
                    break;

                default:
                    throw new NotImplementedException($"Not implemented for numeric type {min.numericType}");
            }
        }
    }
}