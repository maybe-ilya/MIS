using System;
using UnityEngine;

namespace mis.Core
{
    public sealed class GameEntity : MonoBehaviour,
        IComparable<GameEntity>,
        IEquatable<GameEntity>
    {
        private GameId _typeId;

        public uint Id { get; set; }

        public GameId TypeId
        {
            get => _typeId;
            set
            {
                if (_typeId != GameId.ZERO_ID)
                {
                    throw new Exception($"{nameof(GameEntity)} {nameof(TypeId)} should be set once");
                }

                _typeId = value;
            }
        }

        public int CompareTo(GameEntity other)
        {
            var idComp = Id.CompareTo(other.Id);
            var typeIdComp = TypeId.CompareTo(other.TypeId);

            return idComp == 0 ? typeIdComp : idComp;
        }


        public bool Equals(GameEntity other) =>
            Id == other.Id && TypeId == other.TypeId;

        public override bool Equals(object other) =>
            other is GameEntity entity && Equals(entity);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 7;
                hash = hash * 13 + Id.GetHashCode();
                hash = hash * 13 + TypeId.GetHashCode();
                return hash;
            }
        }
    }
}
