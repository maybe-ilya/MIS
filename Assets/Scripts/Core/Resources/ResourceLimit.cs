using System;
using UnityEngine;

namespace mis.Core
{
    [Serializable]
    public struct ResourceLimit :
        IEquatable<ResourceLimit>,
        IComparable<ResourceLimit>
    {
        [SerializeField]
        private GameId _id;
        [SerializeField]
        private UintRange _range;

        public GameId Id => _id;

        public UintRange Range => _range;

        public ResourceLimit(GameId id, UintRange range)
        {
            _id = id;
            _range = range;
        }

        public Resource LimitResource(Resource resource)
        {
            return new Resource(resource.Id, Range.Clamp(resource.Value));
        }

        public bool Equals(ResourceLimit other) => Id == other.Id && Range == other.Range;

        public override bool Equals(object obj) => obj is ResourceLimit other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 7;
                hash = hash * 13 + Id.GetHashCode();
                hash = hash * 13 + Range.GetHashCode();
                return hash;
            }
        }

        public override string ToString() => $"[{Id}, {Range}]";

        public int CompareTo(ResourceLimit other)
        {
            var idCompare = Id.CompareTo(other.Id);
            var rangeCompare = Range.CompareTo(other.Range);

            return idCompare != 0 ? idCompare : rangeCompare;
        }

        public static bool operator ==(ResourceLimit left, ResourceLimit right) => left.Equals(right);

        public static bool operator !=(ResourceLimit left, ResourceLimit right) => !left.Equals(right);


        public static implicit operator ResourceLimit((GameId, UintRange) tuple) => new(tuple.Item1, tuple.Item2);
    }
}