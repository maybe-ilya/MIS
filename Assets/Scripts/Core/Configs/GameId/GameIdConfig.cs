using System.Collections.Generic;
using UnityEngine;

namespace mis.Core
{
    public class GameIdConfig : PreloadedScriptableSignleton<GameIdConfig>, ISerializationCallbackReceiver
    {
        [SerializeField]
        [HideInInspector]
        private GameIdEntry[] _entries;

#if UNITY_EDITOR
        public GameIdEntry[] Entries => _entries;
#endif

        private readonly Dictionary<string, int> _valuesByNames = new();
        private readonly Dictionary<int, string> _namesByValues = new();

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            _valuesByNames.Clear();
            _namesByValues.Clear();

            foreach (var entry in _entries)
            {
                _valuesByNames.Add(entry.Name, entry.Value);
                _namesByValues.Add(entry.Value, entry.Name);
            }
        }

        public string GetNameByValue(GameId id) =>
            _namesByValues.TryGetValue(id, out var result) ? result : GameTagConsts.TAG_NOT_SET_LABEL;

        public int GetValueByName(string name) =>
            _valuesByNames.GetValueOrDefault(name);
    }
}
