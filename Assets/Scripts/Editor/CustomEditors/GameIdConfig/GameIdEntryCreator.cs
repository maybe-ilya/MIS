using UnityEngine;
using UnityEditor;
using Cysharp.Threading.Tasks;
using mis.Core;
using System.Threading;
using System;
using System.Linq;

namespace mis.Editor
{
    public struct GameIdConfigCreationResult
    {
        public (string NewIdName, string ConstName)[] Data;
    }

    public class GameIdEntryCreator : EditorWindow
    {
        [Serializable]
        private class NewIdEntryData
        {
            public string Name;
            public string ConstName;
        }

        [SerializeField]
        [OneLine]
        private NewIdEntryData[] _newEntries;

        private Vector2 _scroll;
        private SerializedObject _serializedObject;

        private UniTaskCompletionSource<GameIdConfigCreationResult> _completionSource;
        private CancellationTokenSource _cancelTokenSource;

        private void Awake()
        {
            _completionSource = new UniTaskCompletionSource<GameIdConfigCreationResult>();
            _cancelTokenSource = new CancellationTokenSource();
        }

        private void OnEnable()
        {
            titleContent = "Create New Id".ToGUI();
            _serializedObject = new SerializedObject(this);
        }

        private void OnDisable()
        {
            _serializedObject = null;
            CancelTask(false);
        }

        private void OnGUI()
        {
            using var scrollScope = new GUILayout.ScrollViewScope(_scroll);
            DrawDefaultInspector();
            DrawButtons();
            _scroll = scrollScope.scrollPosition;
        }

        private void DrawDefaultInspector()
        {
            _serializedObject.UpdateIfRequiredOrScript();

            var iterator = _serializedObject.GetIterator();
            var enterChildren = true;

            while (iterator.NextVisible(enterChildren))
            {
                enterChildren = false;

                switch (iterator.propertyPath)
                {
                    case "m_Script":
                    case "m_SerializedDataModeController":
                        continue;
                }

                EditorGUILayout.PropertyField(iterator, includeChildren: true);
            }
            _serializedObject.ApplyModifiedProperties();
        }

        private void DrawButtons()
        {
            using var _ = new EditorGUILayout.HorizontalScope();

            using (new EditorGUI.DisabledScope(!AreAllNewDataValid()))
            {
                if (GUILayout.Button("Create"))
                {
                    FinishTask();
                }
            }

            if (GUILayout.Button("Cancel"))
            {
                CancelTask(true);
            }
        }

        private bool AreAllNewDataValid() =>
            _newEntries?.All(item => item.Name.IsFilled()) ?? false;

        private void FinishTask()
        {
            if (_completionSource == null)
            {
                return;
            }

            var result = new GameIdConfigCreationResult()
            {
                Data = _newEntries.Select(item => (item.Name, item.ConstName)).ToArray()
            };

            _completionSource.TrySetResult(result);
            _completionSource = null;
            Close();
        }

        private void CancelTask(bool shouldCloseWindow)
        {
            _completionSource?.TrySetCanceled(_cancelTokenSource.Token);

            if (shouldCloseWindow)
            {
                Close();
            }
        }

        public static UniTask<GameIdConfigCreationResult> CreateNew()
        {
            var window = CreateInstance<GameIdEntryCreator>();
            var task = window._completionSource.Task;
            window.ShowUtility();
            return task;
        }
    }
}
