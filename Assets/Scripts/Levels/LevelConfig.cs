using mis.Core;
using UnityEngine;

namespace mis.Levels
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "mis Assets/LevelConfig")]
    internal sealed class LevelConfig : AbstractConfig, ILevelConfig
    {
        [SerializeField]
        [SceneName]
        private string _sceneName;

        [SerializeField]
        private GameId _pawnId;

        [SerializeField]
        private GameId _hudId;

        public string SceneName => _sceneName;

        public GameId PlayerPawnId => _pawnId;

        public GameId HUDId => _hudId;
    }
}