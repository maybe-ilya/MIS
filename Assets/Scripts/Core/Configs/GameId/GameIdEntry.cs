using System;
using UnityEngine;

namespace mis.Core
{
    [Serializable]
    public sealed class GameIdEntry
    {
        [SerializeField]
        private string _name;

        [SerializeField]
        [ReadOnly]
        private int _value;

        [SerializeField]
        private string _constName;

        public string Name => _name;

        public int Value => _value;

        public string ConstName => _constName;

        public GameIdEntry() : this(0, "New Entry") { }

        public GameIdEntry(int value, string name, string constName = null)
        {
            _value = value;
            _name = name;
            _constName = constName.IsFilled() ? constName : string.Empty;
        }
    }
}
