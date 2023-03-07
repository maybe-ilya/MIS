using UnityEditor;
using Cysharp.Threading.Tasks;
using mis.Core;
using URandom = UnityEngine.Random;

namespace mis.Editor
{
    public class GameIdEntryTableView : SerializedArrayTableView
    {
        public GameIdEntryTableView(SerializedProperty arrayProperty) : base(arrayProperty) { }

        protected override void AddElement()
        {
            TryToAddNewElementAsync().Forget();
        }

        private async UniTaskVoid TryToAddNewElementAsync()
        {
            var creationResult = await GameIdEntryCreator.CreateNew();

            foreach ((var newIdName, var constName) in creationResult.Data)
            {
                var idValue = URandom.Range(int.MinValue, int.MaxValue);
                var index = _arrayProperty.arraySize++;
                _arrayProperty.GetArrayElementAtIndex(index).boxedValue = new GameIdEntry(
                    value: idValue,
                    name: newIdName,
                    constName: constName
                    );
            }

            Reload();
        }
    }
}