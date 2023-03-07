using mis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace mis.GameSettings
{
    [Serializable]
    internal sealed class GameSettingsData
    {
        [SerializeField]
        [OneLine]
        private FloatSetting[] _floatSettings;

        [SerializeField]
        [OneLine]
        private BoolSetting[] _boolSettings;

        private Dictionary<GameId, FloatSetting> _floatSettingsMap;
        private Dictionary<GameId, BoolSetting> _boolSettingsMap;

        private Dictionary<GameId, FloatSetting> FloatSettingsMap =>
            _floatSettingsMap ??= _floatSettings.ToDictionary(entry => entry.Id, entry => entry);

        private Dictionary<GameId, BoolSetting> BoolSettingsMap =>
            _boolSettingsMap ??= _boolSettings.ToDictionary(entry => entry.Id, entry => entry);

        public GameSettingsData(GameSettingsData other)
        {
            _floatSettings = other._floatSettings.Select(x => new FloatSetting(x)).ToArray();
            _boolSettings = other._boolSettings.Select(x => new BoolSetting(x)).ToArray();
        }

        public float GetFloatSettingValue(GameId gameId) =>
            FloatSettingsMap[gameId].Value;

        public bool GetBoolSettingValue(GameId gameId) =>
            BoolSettingsMap[gameId].Value;

        public bool TryGetFloatSettingValue(GameId settingId, out float value)
        {
            if (FloatSettingsMap.TryGetValue(settingId, out var floatSetting))
            {
                value = floatSetting.Value;
                return true;
            }

            value = 0.0f;
            return false;
        }

        public bool TryGetBoolSettingValue(GameId settingId, out bool value)
        {
            if (BoolSettingsMap.TryGetValue(settingId, out var boolSetting))
            {
                value = boolSetting.Value;
                return true;
            }

            value = false;
            return false;
        }

        public void SetFloatSettingValue(GameId settingId, float value)
        {
            if (!FloatSettingsMap.TryGetValue(settingId, out var floatSetting))
            {
                return;
            }

            floatSetting.Value = value;
        }

        public void SetBoolSettingValue(GameId settingId, bool value)
        {
            if (!BoolSettingsMap.TryGetValue(settingId, out var boolSetting))
            {
                return;
            }

            boolSetting.Value = value;
        }
    }
}