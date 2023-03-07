using System.Collections.Generic;
using System.Linq;
using mis.Core;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace mis.Objects
{
    public class ObjectService : IObjectService, IStartableService
    {
        private readonly IConfigService _configService;
        private readonly Dictionary<uint, GameEntity> _objectsById;
        private readonly Dictionary<GameId, GameEntityPool> _gameIdPools;
        private GameObject _poolHolder;
        private uint _lastObjectId;

        public int StartPriority => 0;

        public ObjectService(IConfigService configService)
        {
            _configService = configService;
            _objectsById = new Dictionary<uint, GameEntity>();
            _gameIdPools = new Dictionary<GameId, GameEntityPool>();
        }

        void IStartableService.OnServiceStart()
        {
            _poolHolder = new GameObject("[OBJECT POOL HOLDER]");
            UObject.DontDestroyOnLoad(_poolHolder);
        }

        public GameEntity GetEntityById(uint id) =>
            _objectsById[id];

        public GameEntity SpawnEntityByType(GameId type, Transform parent = null, string name = null)
        {
            var gameEntity = GetPool(type).Get();

            if (parent != null)
            {
                gameEntity.transform.SetParent(parent, false);
            }

            if (name.IsFilled())
            {
                gameEntity.gameObject.name = name;
            }

            return gameEntity;
        }

        public T SpawnEntityByType<T>(GameId type, Transform parent = null, string name = null) where T : IGameEntityComponent =>
            SpawnEntityByType(type, parent, name).gameObject.GetComponent<T>();

        public void DespawnEntity(GameEntity gameEntity)
        {
            var typeId = gameEntity.TypeId;
            if (typeId == GameId.ZERO_ID)
            {
                UObject.Destroy(gameEntity.gameObject);
                return;
            }

            GetPool(typeId).Release(gameEntity);
        }

        public void DespawnEntity<T>(T entityHolder) where T : IGameEntityComponent =>
            DespawnEntity(entityHolder.Entity);

        private uint GetNewObjectId() => ++_lastObjectId;

        private GameEntityPool GetPool(GameId type)
        {
            if (!_gameIdPools.TryGetValue(type, out var gameEntityPool))
            {
                var objectCfg = _configService.GetConfig<IObjectConfig>(type);
                _gameIdPools[type] = gameEntityPool = new GameEntityPool(
                    createFunc: () => InstantiateNewEntity(type),
                    actionOnGet: OnGetEntity,
                    actionOnRelease: OnReleaseEntity,
                    actionOnDestroy: OnDestroyEntity,
                    defaultCapacity: objectCfg.MinPooledObjects,
                    maxSize: objectCfg.MaxPooledObjects);
            }

            return gameEntityPool;
        }

        private GameEntity InstantiateNewEntity(GameId type)
        {
            var cfg = _configService.GetConfig<IObjectConfig>(type);
            var go = UObject.Instantiate(cfg.Prefab);

            var gameEntity = go.AddComponent<GameEntity>();
            gameEntity.TypeId = type;

            return gameEntity;
        }

        private void OnGetEntity(GameEntity gameEntity)
        {
            var objId = GetNewObjectId();
            gameEntity.Id = objId;
            _objectsById.Add(objId, gameEntity);
            TryInitializeResources(gameEntity);
            gameEntity.transform.SetParent(null, false);
            gameEntity.gameObject.SetActive(true);
        }

        private void OnReleaseEntity(GameEntity gameEntity)
        {
            var objId = gameEntity.Id;
            _objectsById.Remove(objId);
            gameEntity.gameObject.SetActive(false);
            gameEntity.transform.SetParent(_poolHolder.transform, false);
            TryClearResources(gameEntity);
        }

        private void OnDestroyEntity(GameEntity gameEntity) =>
            UObject.Destroy(gameEntity.gameObject);

        private void TryInitializeResources(GameEntity gameEntity)
        {
            var cfg = _configService.GetConfig<IObjectConfig>(gameEntity.TypeId);
            if (!cfg.NeedInitializeResources
                || !gameEntity.TryGetComponent<BaseResourceContainer>(out var resourceContainer))
            {
                return;
            }

            foreach (var resourceLimit in cfg.ResourceLimits)
            {
                resourceContainer.AddLimit(resourceLimit);
            }

            foreach (var resource in cfg.Resources)
            {
                resourceContainer.AddResource(resource);
            }
        }

        private void TryClearResources(GameEntity gameEntity)
        {
            if (!gameEntity.TryGetComponent<BaseResourceContainer>(out var resourceContainer))
            {
                return;
            }

            resourceContainer.ClearAllResourceData();
            resourceContainer.ClearAllLimitData();
        }

        public uint[] FindEntitiesOfType(GameId type)
        {
            var foundIds = new HashSet<uint>();
            foreach ((var id, var entity) in _objectsById)
            {
                if (entity.TypeId == type)
                {
                    foundIds.Add(id);
                }
            }

            return foundIds.ToArray();
        }
    }
}