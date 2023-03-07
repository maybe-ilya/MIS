using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace mis.Editor
{
    public sealed class GameIdPopup : PopupWindowContent
    {
        private Rect _showRect;
        private GameIdListView _idListView;
        private SearchField _searchField;
        private SerializedProperty _idProperty;
        private string _searchString;

        private const int MAX_VISIBLE_ITEMS = 10;

        private static float SingleLineHeight => EditorGUIUtility.singleLineHeight;

        public GameIdPopup(Rect showRect, SerializedProperty idProperty)
        {
            _showRect = showRect;
            _idProperty = idProperty;
        }

        public override void OnOpen()
        {
            _searchField = new SearchField();
            _searchField.downOrUpArrowKeyPressed += SetFocusOnList;
            _searchField.SetFocus();

            _idListView = new GameIdListView(_idProperty.intValue);
            _idListView.OnSelectionChanged += OnValueChanged;
            _idListView.OnDoubleClick += CloseWindow;
            _idListView.OnEnterPress += CloseWindow;

            AssemblyReloadEvents.beforeAssemblyReload += CloseWindow;
        }

        public override void OnClose()
        {
            AssemblyReloadEvents.beforeAssemblyReload -= CloseWindow;

            _idListView.OnEnterPress -= CloseWindow;
            _idListView.OnDoubleClick -= CloseWindow;
            _idListView.OnSelectionChanged -= OnValueChanged;
            _searchField.downOrUpArrowKeyPressed -= SetFocusOnList;
        }

        public override void OnGUI(Rect rect)
        {
            var searchFieldRect = rect;
            searchFieldRect.height = SingleLineHeight;

            var tagListRect = rect;
            tagListRect.height -= SingleLineHeight;
            tagListRect.y += SingleLineHeight;

            using (var searchFieldChange = new EditorGUI.ChangeCheckScope())
            {
                _searchString = _searchField.OnGUI(searchFieldRect, _searchString);

                if (searchFieldChange.changed)
                {
                    _idListView.searchString = _searchString;
                }
            }

            _idListView.OnGUI(tagListRect);
        }

        public override Vector2 GetWindowSize()
        {
            var width = Screen.width;
            var height = SingleLineHeight + Math.Min(_idListView.totalHeight, SingleLineHeight * MAX_VISIBLE_ITEMS);

            return new(width, height);
        }

        private void OnValueChanged(int newValue)
        {
            _idProperty.longValue = newValue;
            _idProperty.serializedObject.ApplyModifiedProperties();
        }

        private void SetFocusOnList()
        {
            _idListView.SetFocusAndEnsureSelectedItem();
        }

        private void CloseWindow()
        {
            if (editorWindow == null)
            {
                return;
            }
            editorWindow.Close();
        }
    }
}
