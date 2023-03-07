using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using mis.Core;

namespace mis.Editor
{
    public class SerializedArrayTableView : TreeView
    {
        protected SerializedProperty _arrayProperty;

        protected static float SingleLineHeight => EditorGUIUtility.singleLineHeight;

        protected static GUIContent PlusIcon => EditorGUIUtility.IconContent(EditorGUIUtility.isProSkin ? "d_Toolbar Plus" : "Toolbar Plus");

        protected static GUIContent MinusIcon => EditorGUIUtility.IconContent(EditorGUIUtility.isProSkin ? "d_Toolbar Minus" : "Toolbar Minus");

        public SerializedArrayTableView(SerializedProperty arrayProperty) : base(new TreeViewState(), CreateNewHeader(arrayProperty.Copy()))
        {
            _arrayProperty = arrayProperty;
            showAlternatingRowBackgrounds = true;
            showBorder = true;
            Reload();
        }

        private static MultiColumnHeader CreateNewHeader(SerializedProperty arrayProperty)
        {
            var bindingFlag = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            var targetObjectType = arrayProperty.serializedObject.targetObject.GetType();
            var collectionType = targetObjectType.GetField(arrayProperty.name, bindingFlag).FieldType;
            var elementType = collectionType.IsGenericType ? collectionType.GetGenericArguments()[0] : collectionType.GetElementType();

            var serializedFields = elementType.GetFields(bindingFlag)
                .Where(field => !Attribute.IsDefined(field, typeof(HideInInspector), true))
                .Where(field => field.IsPublic || Attribute.IsDefined(field, typeof(SerializeField), true));

            var columns = serializedFields.Select(field =>
            {
                var name = field.Name.ToNice();
                var tooltip = field.GetCustomAttribute<TooltipAttribute>()?.tooltip ?? string.Empty;

                return new MultiColumnHeaderState.Column()
                {
                    headerContent = new GUIContent(name, tooltip),
                    headerTextAlignment = TextAlignment.Left,
                    autoResize = true,
                    canSort = false,
                    sortedAscending = false,
                };
            }).ToList();

            columns.Insert(0, new MultiColumnHeaderState.Column()
            {
                headerContent = "#".ToGUI(),
                headerTextAlignment = TextAlignment.Left,
                autoResize = true,
                canSort = false,
                sortedAscending = false,
                allowToggleVisibility = false,
            });

            return new MultiColumnHeader(new MultiColumnHeaderState(columns.ToArray()));
        }

        protected override TreeViewItem BuildRoot()
        {
            var root = new TreeViewItem(-1, -1);
            var items = Enumerable.Range(0, _arrayProperty.arraySize).Select(id => new TreeViewItem(id, 0)).ToArray();

            SetupParentsAndChildrenFromDepths(root, items);

            return root;
        }

        public override void OnGUI(Rect rect)
        {
            var toolbarRect = rect;
            toolbarRect.height = SingleLineHeight;

            var tableRect = rect;
            tableRect.height -= SingleLineHeight;
            tableRect.y += SingleLineHeight;

            OnToolbarGUI(toolbarRect);
            base.OnGUI(tableRect);
        }

        public void OnGUILayout()
        {
            OnGUI(GUILayoutUtility.GetRect(
                0, multiColumnHeader.state.widthOfAllVisibleColumns,
                0, totalHeight + SingleLineHeight,
                GUILayout.ExpandHeight(false), GUILayout.ExpandWidth(true)));
        }

        private void OnToolbarGUI(Rect rect)
        {
            EditorGUI.LabelField(rect, _arrayProperty.displayName, EditorStyles.boldLabel);

            var buttonsRect = rect;
            buttonsRect.width = SingleLineHeight * 2;
            buttonsRect.x = rect.x + rect.width - buttonsRect.width;
            var buttonsSplit = buttonsRect.SplitHorizontally(2);

            if (GUI.Button(buttonsSplit[0], PlusIcon, EditorStyles.toolbarButton))
            {
                AddElement();
            }

            if (GUI.Button(buttonsSplit[1], MinusIcon, EditorStyles.toolbarButton))
            {
                RemoveElement();
            }
        }

        protected override void RowGUI(RowGUIArgs args)
        {
            var index = args.item.id;
            var property = _arrayProperty.GetArrayElementAtIndex(index);
            var childIndex = 0;
            var columnIndex = 0;
            var enterChildren = true;

            EditorGUI.LabelField(args.GetCellRect(0), (index + 1).ToString().ToGUI());

            while (property.Next(enterChildren))
            {
                enterChildren = false;
                columnIndex = childIndex + 1;

                if (multiColumnHeader.IsColumnVisible(columnIndex))
                {
                    EditorGUI.PropertyField(args.GetCellRect(columnIndex), property, GUIContent.none);
                }

                childIndex++;
            }
        }

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
                case { keyCode: KeyCode.N, modifiers: EventModifiers.Control | EventModifiers.Alt }:
                    AddElement();
                    isEventUsed = true;
                    break;

                case { keyCode: KeyCode.Delete, modifiers: EventModifiers.FunctionKey }:
                    RemoveElement();
                    isEventUsed = true;
                    break;
            }

            if (isEventUsed)
            {
                current.Use();
                GUIUtility.ExitGUI();
            }
        }

        protected virtual void AddElement()
        {
            _arrayProperty.arraySize++;
            Reload();
        }

        protected virtual void RemoveElement()
        {
            var selectedIds = GetSelection().OrderByDescending(x => x).ToArray();
            foreach (var index in selectedIds)
            {
                _arrayProperty.DeleteArrayElementAtIndex(index);
            }
            Reload();
        }
    }
}