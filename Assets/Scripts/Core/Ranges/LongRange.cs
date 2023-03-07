using System;
using UnityEngine;

namespace mis.Core
{
    [Serializable]
    public struct LongRange :
        IRange<long>,
        IComparable<LongRange>,
        IEquatable<LongRange>
    {
        [SerializeField] private long _min, _max;

        public long Min => _min;

        public long Max => _max;

        public LongRange(long min, long max)
        {
            _min = min;
            _max = max;
        }

        public long Clamp(long value) => Math.Min(Math.Max(value, Min), Max);

        public bool IsInside(long value) => value >= Min && value <= Max;

        public bool IsOutside(long value) => value < Min || value > Max;

        public long Lerp(float step) => Min + (long)((Max - Min) * step);

        public override bool Equals(object obj) => obj is LongRange other && Equals(other);

        public bool Equals(LongRange other) => Min == other.Min && Max == other.Max;

        public static bool operator ==(LongRange left, LongRange right) => left.Equals(right);

        public static bool operator !=(LongRange left, LongRange right) => !left.Equals(right);

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

        public int CompareTo(LongRange other)
        {
            int minCompare = Min.CompareTo(other.Min);
            int maxCompare = Max.CompareTo(other.Max);

            return minCompare != 0 ? minCompare : maxCompare;
        }

        public override string ToString() => $"[{Min}, {Max}]";

        public static implicit operator LongRange(long value) => new(0, value);

        public static implicit operator LongRange((long, long) tuple) => new(tuple.Item1, tuple.Item2);
    }
}