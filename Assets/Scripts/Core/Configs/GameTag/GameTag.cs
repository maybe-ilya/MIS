using System;
using UnityEngine;

namespace mis.Core
{
    [Serializable]
    public struct GameTag
    {
        [SerializeField]
        [HideInInspector]
        private long _value;

        public long Value => _value;

        public GameTag(long value) => _value = value;

        public static implicit operator long(GameTag tag) => tag.Value;
        public static explicit operator GameTag(long value) => new (value);
    }
}
