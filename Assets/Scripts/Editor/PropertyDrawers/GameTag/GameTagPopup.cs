using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace mis.Editor
{
    public sealed class GameTagPopup : PopupWindowContent
    {
        private Rect _showRect;
        private GameTagListView _tagListView;
        private SearchField _searchField;
        private SerializedProperty _tagProperty;
        private readonly bool _canMultiSelect;
        private string _searchString;

        private const int MAX_VISIBLE_ITEMS = 10;

        private static float SingleLineHeight => EditorGUIUtility.singleLineHeight;

        public GameTagPopup(Rect showRect, SerializedProperty tagProperty, bool canMultiSelect)
        {
            _showRect = showRect;
            _tagProperty = tagProperty;
            _canMultiSelect = canMultiSelect;
        }

        public override void OnOpen()
        {
            _searchField = new SearchField();
            _tagListView = new GameTagListView(_tagProperty.longValue, _canMultiSelect);
            _tagListView.OnSelectionChanged += OnValueChanged;
            _tagListView.SetFocus();
        }

        public override void OnGUI(Rect rect)
        {
            var searchFieldRect = rect;
            searchFieldRect.height = SingleLineHeight;

            var tagListRect = rect;
            tagListRect.height -= SingleLineHeight;
            tagListRect.y += SingleLineHeight;

            using(var searchFieldChange = new EditorGUI.ChangeCheckScope())
            {
                _searchString = _searchField.OnGUI(searchFieldRect, _searchString);

                if (searchFieldChange.changed)
                {
                    _tagListView.searchString = _searchString;
                }
            }

            _tagListView.OnGUI(tagListRect);
        }

        public override Vector2 GetWindowSize()
        {
            var width = _showRect.width;
            var height = SingleLineHeight + Math.Min(_tagListView.totalHeight, SingleLineHeight * MAX_VISIBLE_ITEMS);

            return new(width, height);
        }

        private void OnValueChanged(long newValue)
        {
            _tagProperty.longValue = newValue;
            _tagProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}
