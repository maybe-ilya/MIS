using UnityEngine;
using UnityEditor;
using mis.Core;

namespace mis.Editor
{
	[CustomPropertyDrawer(typeof(Resource))]
	public class ResourcePropertyDrawer : PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using var propertyScope = new EditorGUI.PropertyScope(position, label, property);
            var rect = EditorGUI.PrefixLabel(position, propertyScope.content);
			var split = rect.SplitHorizontally(2);

			property.Next(true);
			property.DrawPropertyFieldOnlyTooltip(split[0]);

			property.Next(false);
			property.DrawPropertyFieldOnlyTooltip(split[1]);
		}		
	}
}