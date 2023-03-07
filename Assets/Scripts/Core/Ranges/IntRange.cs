using System;
using UnityEngine;

namespace mis.Core
{
    [Serializable]
    public struct IntRange :
        IRange<int>,
        IComparable<IntRange>,
        IEquatable<IntRange>
    {
        [SerializeField] private int _min, _max;

        public int Min => _min;

        public int Max => _max;

        public IntRange(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public int Clamp(int value) => Math.Min(Math.Max(value, Min), Max);

        public bool IsInside(int value) => value >= Min && value <= Max;

        public bool IsOutside(int value) => value < Min || value > Max;

        public int Lerp(float step) => Min + (int)((Max - Min) * step);

        public override bool Equals(object obj) => obj is IntRange other && Equals(other);

        public bool Equals(IntRange other) => Min == other.Min && Max == other.Max;

        public static bool operator ==(IntRange left, IntRange right) => left.Equals(right);

        public static bool operator !=(IntRange left, IntRange right) => !left.Equals(right);

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

        public int CompareTo(IntRange other)
        {
            int minCompare = Min.CompareTo(other.Min);
            int maxCompare = Max.CompareTo(other.Max);

            return minCompare != 0 ? minCompare : maxCompare;
        }

        public override string ToString() => $"[{Min}, {Max}]";

        public static implicit operator IntRange(int value) => new(0, value);

        public static implicit operator IntRange((int, int) tuple) => new(tuple.Item1, tuple.Item2);
    }
}