using System;
using UnityEngine;
using UnityEditor;
using mis.Core;

namespace mis.Editor
{
    [CustomPropertyDrawer(typeof(CheckObjectAttribute))]
    public class CheckObjectAttributePropertyDrawer : PropertyDrawer
    {
        private static CheckObjectAttributeSettings Settings => CheckObjectAttributeSettings.Instance;

        private CheckObjectAttribute CheckAttr => attribute as CheckObjectAttribute;

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            if (!IsPropertySupported(property))
            {
                throw new Exception($"{property.propertyType} is not supported");
            }

            using (GetScope(property))
            {
                EditorGUI.PropertyField(rect, property, label, property.hasVisibleChildren);
            }
        }

        private IDisposable GetScope(SerializedProperty property)
        {
            var color = GetPropertyColor(property);

            return CheckAttr.Highlight switch
            {
                CheckHighlight.Background => new GUIBackgroundColorScope(color),
                CheckHighlight.Content => new GUIContentColorScope(color),
                _ => new GUIColorScope(color),
            };
        }

        private Color GetPropertyColor(SerializedProperty property) =>
            IsPropertyValid(property) ? Settings.ValidColor : Settings.InvalidColor;

        private bool IsPropertySupported(SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.ObjectReference:
                case SerializedPropertyType.ExposedReference:
                case SerializedPropertyType.ManagedReference:
                    return true;

                default:
                    return false;
            }
        }

        private bool IsPropertyValid(SerializedProperty property)
        {
            return property.propertyType switch
            {
                SerializedPropertyType.ObjectReference => property.objectReferenceValue != null,
                SerializedPropertyType.ExposedReference => property.exposedReferenceValue != null,
                SerializedPropertyType.ManagedReference => property.managedReferenceValue != null,
                _ => true,
            };
        }
    }
}