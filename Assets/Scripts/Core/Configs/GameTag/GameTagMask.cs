using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mis.Core
{
    [Serializable]
    public struct GameTagMask
    {
        [SerializeField]
        [HideInInspector]
        private long _value;

        public long Value => _value;

        public GameTagMask(long value) => _value = value;

        public bool HasTag(GameTag tag)
        {
            var checkValue = (long)1 << (int)tag.Value;
            return (checkValue & Value) == checkValue;
        }

        public List<int> GetNonZeroBitIndices()
        {
            var result = new List<int>();

            for(var index = 0; index < GameTagConsts.MAX_TAG_COUNT; ++index)
            {
                if (HasTag((GameTag)index))
                {
                    result.Add(index);
                }
            }

            return result;
        }

        public static implicit operator long(GameTagMask tag) => tag.Value;

        public static explicit operator GameTagMask(long value) => new(value);      
    }
}
