using mis.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace mis.GameSettings
{
    [CreateAssetMenu(fileName = "GameSettingsConfig", menuName = "mis Assets/GameSettingsConfig")]
    internal sealed class GameSettingsConfig : AbstractConfig, ISerializationCallbackReceiver
    {
        [SerializeField]
        private GameSettingsData _initialSettings;

        [SerializeField]
        [OneLine]
        private FloatSettingLimit[] _floatSettingLimits;

        private Dictionary<GameId, FloatSettingLimit> _floatSettingLimitsMap;

        public GameSettingsData InitialSettings => _initialSettings;

        public void OnAfterDeserialize()
        {
            _floatSettingLimitsMap = _floatSettingLimits.ToDictionary(entry => entry.SettingId, entry => entry);
        }

        public void OnBeforeSerialize() { }

        public FloatSettingLimit GetFloatSettingLimit(GameId gameId) =>
            _floatSettingLimitsMap[gameId];
    }
}