using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.IMGUI.Controls;
using mis.Core;
using UnityEngine;

namespace mis.Editor
{
    public class GameIdListView : TreeView
    {
        public event Action<int> OnSelectionChanged;
        public event Action OnDoubleClick;
        public event Action OnEnterPress;

        public GameIdListView(int selectedId) : base(new TreeViewState()
        {
            selectedIDs = new List<int> { selectedId },
        })
        {
            showAlternatingRowBackgrounds = true;
            Reload();
        }

        protected override TreeViewItem BuildRoot()
        {
            var root = new TreeViewItem(-1, -1);
            var items = GameIdConfig.Instance.Entries.Select(id => new TreeViewItem(id.Value, 0, id.Name)).ToList();

            SetupParentsAndChildrenFromDepths(root, items);
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
