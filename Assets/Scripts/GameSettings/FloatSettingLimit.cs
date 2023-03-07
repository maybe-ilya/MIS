using mis.Core;
using System;
using UnityEngine;

namespace mis.GameSettings
{
    [Serializable]
    public sealed class FloatSettingLimit : IEquatable<FloatSettingLimit>
    {
        [SerializeField]
        private GameId _settingId;
        [SerializeField]
        private FloatRange _range;

        public GameId SettingId => _settingId;

        public FloatRange Range => _range;

        public override bool Equals(object obj) =>
            obj is FloatSettingLimit other && Equals(other);

        public bool Equals(FloatSettingLimit other) =>
            SettingId == other.SettingId && Range.Equals(other.Range);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 7;
                hash = hash * 13 + SettingId.GetHashCode();
                hash = hash * 13 + Range.GetHashCode();
                return hash;
            }
        }

        public override string ToString() =>
            $"[{SettingId}, {Range}]";
    }
}