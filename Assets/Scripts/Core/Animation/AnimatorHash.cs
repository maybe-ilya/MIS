using System;
using UnityEngine;

namespace mis.Core
{
    [Serializable]
    public struct AnimatorHash :
        IEquatable<AnimatorHash>,
        IComparable<AnimatorHash>
    {
        [SerializeField]
        private string _name;

        [SerializeField]
        [HideInInspector]
        private int _value;

        public string Name => _name;

        public int Value => _value;

        public AnimatorHash(string name)
        {
            _name = name;
            _value = Animator.StringToHash(_name);
        }

        public override bool Equals(object obj) =>
            obj is AnimatorHash hash && Equals(hash);

        public bool Equals(AnimatorHash other) =>
            Value == other.Value;

        public override int GetHashCode() =>
            Value.GetHashCode();

        public int CompareTo(AnimatorHash other) =>
            Value.CompareTo(other.Value);

        public override string ToString() =>
            $"'{Name}' hash = {Value}";

        public static bool operator ==(AnimatorHash left, AnimatorHash right) =>
            left.Equals(right);

        public static bool operator !=(AnimatorHash left, AnimatorHash right) =>
            !left.Equals(right);

        public static implicit operator AnimatorHash(string input) =>
            new(input);

        public static implicit operator int(AnimatorHash hash) =>
            hash.Value;
    }
}