using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.IMGUI.Controls;
using mis.Core;

namespace mis.Editor
{
    public class GameTagListView : TreeView
    {
        private readonly bool _canMultiSelect;
        public event Action<long> OnSelectionChanged;

        public GameTagListView(long input, bool canMultiSelect) : base(
            new TreeViewState()
            {
                selectedIDs = canMultiSelect 
                ? ((GameTagMask)input).GetNonZeroBitIndices() 
                : new List<int> { (int)input },
            })
        {
            _canMultiSelect = canMultiSelect;
            showAlternatingRowBackgrounds = true;
            Reload();
        }

        protected override bool CanMultiSelect(TreeViewItem item) => _canMultiSelect;

        protected override TreeViewItem BuildRoot()
        {
            var root = new TreeViewItem(-1, -1);
            var items = new List<TreeViewItem>();
            var tagNames = GameTagConfig.Instance.GetTagNames();
            for (var index = 0; index < tagNames.Length; ++index)
            {
                var tagName = tagNames[index];
                if (!tagName.IsFilled())
                {
                    continue;
                }
                items.Add(new TreeViewItem(index, 0, tagName));
            }

            SetupParentsAndChildrenFromDepths(root, items);
            return root;
        }

        protected override void SelectionChanged(IList<int> selectedIds)
        {
            long result;
            if (_canMultiSelect)
            {
                result = selectedIds.Sum(value => (long)1 << value);
            }
            else
            {
                result = selectedIds[0];
            }

            OnSelectionChanged?.Invoke(result);
        }
    }
}
