using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace mis.Editor
{
    public sealed class SceneNamePopup : PopupWindowContent
    {
        private Rect _showRect;
        private SerializedProperty _sceneNameProperty;
        private string[] _sceneAssetPaths;
        private SceneNameList _sceneNameList;
        private SearchField _searchField;
        private string _searchString;

        private const int MAX_VISIBLE_ITEMS = 10;

        private static float SingleLineHeight => EditorGUIUtility.singleLineHeight;

        public SceneNamePopup(Rect showRect, SerializedProperty sceneNameProperty)
        {
            _showRect = showRect;
            _sceneNameProperty = sceneNameProperty;
            _sceneAssetPaths = AssetDatabase.FindAssets($"t:{nameof(SceneAsset)}", new[] { "Assets/" })
                .Select(AssetDatabase.GUIDToAssetPath)
                .ToArray();
        }

        public override void OnOpen()
        {
            _searchField = new SearchField();
            _searchField.downOrUpArrowKeyPressed += SetFocusOnList;
            _searchField.SetFocus();

            _sceneNameList = new SceneNameList(_sceneAssetPaths, _sceneNameProperty.stringValue);
            _sceneNameList.OnSelectionChanged += OnSelectionChanged;
            _sceneNameList.OnDoubleClick += CloseWindow;
            _sceneNameList.OnEnterPress += CloseWindow;

            AssemblyReloadEvents.beforeAssemblyReload += CloseWindow;
        }

        public override void OnClose()
        {
            AssemblyReloadEvents.beforeAssemblyReload -= CloseWindow;

            _sceneNameList.OnEnterPress -= CloseWindow;
            _sceneNameList.OnDoubleClick -= CloseWindow;
            _sceneNameList.OnSelectionChanged -= OnSelectionChanged;
            _searchField.downOrUpArrowKeyPressed -= SetFocusOnList;
        }

        public override void OnGUI(Rect rect)
        {
            var searchFieldRect = rect;
            searchFieldRect.height = SingleLineHeight;

            var sceneNameListRect = rect;
            sceneNameListRect.height -= SingleLineHeight;
            sceneNameListRect.y += SingleLineHeight;

            using (var searchFieldChange = new EditorGUI.ChangeCheckScope())
            {
                _searchString = _searchField.OnGUI(searchFieldRect, _searchString);

                if (searchFieldChange.changed)
                {
                    _sceneNameList.searchString = _searchString;
                }
            }

            _sceneNameList.OnGUI(sceneNameListRect);
        }

        public override Vector2 GetWindowSize()
        {
            var width = Screen.width;
            var height = SingleLineHeight + Math.Min(_sceneNameList.totalHeight, SingleLineHeight * MAX_VISIBLE_ITEMS);

            return new(width, height);
        }

        private void OnSelectionChanged(int selectedIndex)
        {
            _sceneNameProperty.stringValue = selectedIndex >= 0 ? _sceneAssetPaths[selectedIndex] : string.Empty;
            _sceneNameProperty.serializedObject.ApplyModifiedProperties();
        }

        private void SetFocusOnList() =>
            _sceneNameList.SetFocusAndEnsureSelectedItem();

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