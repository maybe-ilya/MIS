using UnityEngine;

namespace mis.Core
{
    public abstract class AbstractGameEntityComponent : MonoBehaviour, IGameEntityComponent
    {
        private GameEntity _gameEntity = null;

        public GameEntity Entity => _gameEntity != null ? _gameEntity : _gameEntity = GetComponent<GameEntity>();
    }
}