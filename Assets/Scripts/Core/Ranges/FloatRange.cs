using System;
using UnityEngine;

namespace mis.Core
{
    [Serializable]
    public struct FloatRange :
        IRange<float>,
        IComparable<FloatRange>,
        IEquatable<FloatRange>
    {
        [SerializeField] private float _min, _max;

        public float Min => _min;

        public float Max => _max;

        public FloatRange(float min, float max)
        {
            _min = min;
            _max = max;
        }

        public float Clamp(float value) => Math.Min(Math.Max(value, Min), Max);

        public bool IsInside(float value) => value >= Min && value <= Max;

        public bool IsOutside(float value) => value < Min || value > Max;

        public float Lerp(float step) => Min + (Max - Min) * step;

        public float GetRatio(float input)
        {
            var value = input - Min;
            var divider = Max - Min;
            return value / divider;
        }

        public override bool Equals(object obj) => obj is FloatRange other && Equals(other);

        public bool Equals(FloatRange other) => Min == other.Min && Max == other.Max;

        public static bool operator ==(FloatRange left, FloatRange right) => left.Equals(right);

        public static bool operator !=(FloatRange left, FloatRange right) => !left.Equals(right);

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

        public int CompareTo(FloatRange other)
        {
            int minCompare = Min.CompareTo(other.Min);
            int maxCompare = Max.CompareTo(other.Max);

            return minCompare != 0 ? minCompare : maxCompare;
        }

        public override string ToString() => $"[{Min}, {Max}]";

        public static implicit operator FloatRange(float value) => new(0.0f, value);

        public static implicit operator FloatRange((float, float) tuple) => new(tuple.Item1, tuple.Item2);
    }
}