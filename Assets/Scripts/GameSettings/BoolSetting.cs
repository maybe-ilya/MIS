using mis.Core;
using System;
using UnityEngine;

namespace mis.GameSettings
{
    [Serializable]
    public sealed class BoolSetting : IEquatable<BoolSetting>
    {
        [SerializeField]
        private GameId _id;
        [SerializeField]
        private bool _value;

        public GameId Id => _id;

        public bool Value
        {
            get => _value;
            set => _value = value;
        }

        public BoolSetting(BoolSetting other)
        {
            _id = other._id;
            _value = other._value;
        }

        public override bool Equals(object obj) =>
            obj is BoolSetting other && Equals(other);

        public bool Equals(BoolSetting other) =>
            Id == other.Id && Value == other.Value;

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 7;
                hash = hash * 13 + Id.GetHashCode();
                hash = hash * 13 + Value.GetHashCode();
                return hash;
            }
        }

        public override string ToString() =>
            $"[{Id}, {Value}]";
    }
}