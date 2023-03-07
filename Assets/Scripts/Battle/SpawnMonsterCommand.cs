using UnityEngine;
using mis.Core;

namespace mis.Battle
{
    public class SpawnMonsterCommand : AbstractCommand
    {
        private readonly GameId _monsterId;
        private readonly Transform _spawnTransform;
        private readonly IObjectService _objectService;

        public IMonster SpawnedMonster { get; private set; }

        public SpawnMonsterCommand(GameId monsterId, Transform spawnPoint)
        {
            _monsterId = monsterId;
            _spawnTransform = spawnPoint;
            _objectService = GameServices.Get<IObjectService>();
        }

        protected override void ExecuteInternal()
        {
            SpawnedMonster = _objectService.SpawnEntityByType<IMonster>(_monsterId);
            var monsterTransform = SpawnedMonster.Entity.gameObject.transform;
            monsterTransform.position = _spawnTransform.position;
            monsterTransform.rotation = _spawnTransform.rotation;
        }
    }
}