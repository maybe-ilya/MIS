using UnityEngine;

namespace mis.Core
{
    public interface IObjectService : IService
    {
        GameEntity GetEntityById(uint id);
        GameEntity SpawnEntityByType(GameId type, Transform parent = null, string name = null);
        T SpawnEntityByType<T>(GameId type, Transform parent = null, string name = null) where T : IGameEntityComponent;
        void DespawnEntity(GameEntity entity);
        void DespawnEntity<T>(T entityHolder) where T : IGameEntityComponent;
        uint[] FindEntitiesOfType(GameId type);
    }
}