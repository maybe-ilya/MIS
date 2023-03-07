using System;
using UnityEngine;

namespace mis.Core
{
    [Serializable]
    public struct UintRange :
        IRange<uint>,
        IComparable<UintRange>,
        IEquatable<UintRange>
    {
        [SerializeField] private uint _min, _max;

        public uint Min => _min;

        public uint Max => _max;

        public UintRange(uint min, uint max)
        {
            _min = min;
            _max = max;
        }

        public uint Clamp(uint value) => Math.Min(Math.Max(value, Min), Max);

        public bool IsInside(uint value) => value >= Min && value <= Max;

        public bool IsOutside(uint value) => value < Min || value > Max;

        public uint Lerp(float step) => Min + (uint)((Max - Min) * step);

        public override bool Equals(object obj) => obj is UintRange other && Equals(other);

        public bool Equals(UintRange other) => Min == other.Min && Max == other.Max;

        public static bool operator ==(UintRange left, UintRange right) => left.Equals(right);

        public static bool operator !=(UintRange left, UintRange right) => !left.Equals(right);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 7;
                hash = hash * 13 + Min.GetHashCode();
                hash = hash * 13 + Max.GetHashCode();
                return hash;
            }
        }

        public int CompareTo(UintRange other)
        {
            int minCompare = Min.CompareTo(other.Min);
            int maxCompare = Max.CompareTo(other.Max);

            return minCompare != 0 ? minCompare : maxCompare;
        }

        public override string ToString() => $"[{Min}, {Max}]";

        public static implicit operator UintRange(uint value) => new(0, value);

        public static implicit operator UintRange((uint, uint) tuple) => new(tuple.Item1, tuple.Item2);
    }
}