using mis.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.IMGUI.Controls;

namespace mis.Editor
{
    public sealed class SceneNameList : TreeView
    {
        public event Action<int> OnSelectionChanged;
        public event Action OnDoubleClick;
        public event Action OnEnterPress;

        private List<TreeViewItem> _items;

        public SceneNameList(string[] sceneNames, string selectedScene) :
            base(GetState(sceneNames, selectedScene))
        {
            _items = sceneNames.Select((sceneName, index) => new TreeViewItem(index, 0, sceneName)).ToList();
            showAlternatingRowBackgrounds = true;
            Reload();
        }

        private static TreeViewState GetState(string[] sceneNames, string selectedScene)
        {
            var selection = new List<int>();

            if (selectedScene.IsFilled())
            {
                var index = Array.IndexOf(sceneNames, selectedScene);
                if (index >= 0)
                {
                    selection.Add(index);
                }
            }

            return new TreeViewState() { selectedIDs = selection };
        }

        protected override TreeViewItem BuildRoot()
        {
            var root = new TreeViewItem(-1, -1);
            SetupParentsAndChildrenFromDepths(root, _items);
            return root;
        }

        protected override bool CanMultiSelect(TreeViewItem item) => false;

        protected override void SelectionChanged(IList<int> selectedIds) =>
            OnSelectionChanged?.Invoke(selectedIds.First());

        protected override void DoubleClickedItem(int id) =>
            OnDoubleClick?.Invoke();

        protected override void KeyEvent()
        {
            var current = Event.current;
            var eventType = current.GetTypeForControl(treeViewControlID);

            if (eventType != EventType.KeyUp)
            {
                return;
            }

            var isEventUsed = false;
            switch (current)
            {
                case { keyCode: KeyCode.Return, modifiers: EventModifiers.None }:
                case { keyCode: KeyCode.Escape, modifiers: EventModifiers.None }:
                    OnEnterPress?.Invoke();
                    isEventUsed = true;
                    break;
            }

            if (isEventUsed)
            {
                current.Use();
                GUIUtility.ExitGUI();
            }
        }
    }
}