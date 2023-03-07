using mis.Core;
using System;
using UnityEngine;

namespace mis.GameSettings
{
    [Serializable]
    public sealed class FloatSetting : IEquatable<FloatSetting>
    {
        [SerializeField]
        private GameId _id;
        [SerializeField]
        private float _value;

        public GameId Id => _id;

        public float Value
        {
            get => _value;
            set => _value = value;
        }

        public FloatSetting(FloatSetting other)
        {
            _id = other._id;
            _value = other._value;
        }

        public override bool Equals(object obj) =>
            obj is FloatSetting other && Equals(other);

        public bool Equals(FloatSetting other) =>
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