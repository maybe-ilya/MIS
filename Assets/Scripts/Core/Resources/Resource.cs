using System;
using UnityEngine;

namespace mis.Core
{
    [Serializable]
    public struct Resource :
        IEquatable<Resource>,
        IComparable<Resource>
    {
        [SerializeField]
        private GameId _id;
        [SerializeField]
        private uint _value;

        public GameId Id => _id;

        public uint Value => _value;

        public Resource(GameId id, uint value)
        {
            _id = id;
            _value = value;
        }

        public bool Equals(Resource other) => Id == other.Id && Value == other.Value;

        public override bool Equals(object obj) => obj is Resource other && Equals(other);

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

        public override string ToString() => $"[{Id}, {Value}]";

        public int CompareTo(Resource other)
        {
            var idCompare = Id.CompareTo(other.Id);
            var valueCompare = Value.CompareTo(other.Value);

            return idCompare != 0 ? idCompare : valueCompare;
        }

        public static implicit operator Resource((GameId, uint) tuple) => new(tuple.Item1, tuple.Item2);

        public static bool operator ==(Resource left, Resource right) => left.Equals(right);

        public static bool operator !=(Resource left, Resource right) => !left.Equals(right);

        public static Resource operator +(Resource left, Resource right)
        {
            if (left.Id != right.Id)
            {
                throw new ArgumentException($"Could not add resource with different Id: left = {left.Id}, right = {right.Id}");
            }

            return new(left.Id, left.Value + right.Value);
        }

        public static Resource operator +(Resource resource, uint value)
        {
            return new(resource.Id, resource.Value + value);
        }

        public static Resource operator -(Resource left, Resource right)
        {
            if (left.Id != right.Id)
            {
                throw new ArgumentException($"Could not subtract resource with different Id: left = {left.Id}, right = {right.Id}");
            }

            return new(left.Id, left.Value - Math.Min(left.Value, right.Value));
        }

        public static Resource operator -(Resource resource, uint value)
        {
            return new(resource.Id, resource.Value - Math.Min(resource.Value, value));
        }

        public static Resource operator *(Resource resource, uint value)
        {
            return new(resource.Id, resource.Value * value);
        }

        public static Resource operator /(Resource resource, uint value)
        {
            return new(resource.Id, resource.Value / value);
        }
    }
}