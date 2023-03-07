using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace mis.Core
{
    public class GameTagConfig : PreloadedScriptableSignleton<GameTagConfig>, ISerializationCallbackReceiver
    {
        [SerializeField]
        [NonReorderable]
        [Delayed]
        private string[] _tagNames = new string[GameTagConsts.MAX_TAG_COUNT];

        private readonly Dictionary<string, long> _valuesByNames = new();
        private readonly Dictionary<long, string> _namesByValues = new();

        public void OnAfterDeserialize()
        {
            _valuesByNames.Clear();
            _namesByValues.Clear();

            for (long index = 0; index < GameTagConsts.MAX_TAG_COUNT; index++)
            {
                var tagName = _tagNames[index];
                if (string.IsNullOrWhiteSpace(tagName))
                {
                    continue;
                }

                _valuesByNames[tagName] = index;
                _namesByValues[index] = tagName;
            }
        }

        public void OnBeforeSerialize() { }

        private void OnValidate()
        {
            Array.Resize(ref _tagNames, GameTagConsts.MAX_TAG_COUNT);
        }

        public string[] GetTagNames() => _tagNames;
        public string GetNameForTag(GameTag tag) => _namesByValues.TryGetValue(tag.Value, out var tagName) ? tagName : GameTagConsts.TAG_NOT_SET_LABEL;
        public string GetNameForTagMask(GameTagMask tagMask)
        {
            if (tagMask.Value == 0)
            {
                return "*not set*";
            }

            var stringBuilder = new StringBuilder();

            for (var index = 0; index < GameTagConsts.MAX_TAG_COUNT; ++index)
            {               
                if (!tagMask.HasTag((GameTag)index))
                {
                    continue;
                }

                var tagName = _tagNames[index];
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append($" | ");
                }
                stringBuilder.Append(tagName);
            }

            if(stringBuilder.Length == 0)
            {
                return GameTagConsts.TAG_NOT_SET_LABEL;
            }

            return stringBuilder.ToString();
        }
    }
}
