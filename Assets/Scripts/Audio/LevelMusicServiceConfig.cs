using mis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace mis.Audio
{
    [CreateAssetMenu(fileName = "LevelMusicHandlerConfig", menuName = "mis Assets/LevelMusicHandlerConfig")]
    internal sealed class LevelMusicServiceConfig : AbstractConfig, ISerializationCallbackReceiver
    {
        [Serializable]
        private struct LevelIdToMusicId
        {
            public GameId LevelId;
            public GameId MusicId;
        }

        [SerializeField]
        [OneLine]
        private LevelIdToMusicId[] _levelMusicData;

        private Dictionary<GameId, GameId> _levelToMusicIdsMap;

        public bool TryGetLevelMusicId(GameId levelId, out GameId musicId) =>
            _levelToMusicIdsMap.TryGetValue(levelId, out musicId);

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            _levelToMusicIdsMap = _levelMusicData.ToDictionary(entry => entry.LevelId, entry => entry.MusicId);
        }
    }
}