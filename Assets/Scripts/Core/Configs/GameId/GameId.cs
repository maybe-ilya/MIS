using System;
using UnityEngine;

namespace mis.Core
{
    [Serializable]
    public struct GameId :
        IEquatable<GameId>,
        IComparable<GameId>
    {
        [SerializeField]
        private int _value;

        public int Value => _value;

        public GameId(int value) => _value = value;

        public static readonly GameId ZERO_ID = new(0);

        public static implicit operator GameId(int value) => new(value);

        public static implicit operator int(GameId id) => id.Value;

        public bool Equals(GameId other) => Value == other.Value;

        public override bool Equals(object obj) => obj is GameId id && Equals(id);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => GameIdConfig.Instance.GetNameByValue(this);

        public int CompareTo(GameId other) => Value.CompareTo(other.Value);
    }
}
