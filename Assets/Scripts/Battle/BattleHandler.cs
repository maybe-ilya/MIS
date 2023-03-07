using mis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace mis.Battle
{
    internal sealed class BattleHandler : IBattleHandler
    {
        private readonly IBattleConfig _battleConfig;
        private readonly GameEntity _targetEntity;
        private readonly IBattleArena _battleArena;
        private readonly IConfigService _configService;
        private readonly IMessageService _messageService;

        private BattleState _battleState;
        private int _currentWaveIndex = -1;
        private List<IMonster> _currentlySpawnedMonsters = new();
        private Queue<GameId> _spawnQueue = new();
        private Queue<int> _lastlyUsedSpawners = new();

        public BattleHandler(
            IBattleConfig battleConfig,
            GameEntity targetEntity,
            IBattleArena battleArena,
            IConfigService configService,
            IMessageService messageService)
        {
            _battleConfig = battleConfig;
            _targetEntity = targetEntity;
            _battleArena = battleArena;
            _configService = configService;
            _messageService = messageService;
        }

        public GameId BattleId => _battleConfig.Id;

        private IBattleWaveData CurrentWave =>
            _battleConfig.Waves[_currentWaveIndex];

        private bool IsCurrentWaveLast =>
            _currentWaveIndex == _battleConfig.WaveCount - 1;

        private bool HasAnythingToSpawn =>
            _spawnQueue.Any();

        private bool AreAnyMonstersSpawned =>
            _currentlySpawnedMonsters.Any();

        public void StartBattle()
        {
            SetBattleState(BattleState.InProgress);
            SubscribeToEvents();
            _battleArena.OnBattleStart();
            StartNextWave();
        }

        public void AbortBattle()
        {
            SetBattleState(BattleState.Aborted);
            UnsubscribeFromEvents();
            _battleArena.OnBattleStop();
            ReleaseResources();
        }

        public void ReleaseResources()
        {
            if (_currentlySpawnedMonsters.Any())
            {
                new DespawnMonstersCommand(_currentlySpawnedMonsters).Execute();
                _currentlySpawnedMonsters.Clear();
            }

            _spawnQueue.Clear();
            _lastlyUsedSpawners.Clear();
        }

        private void StartNextWave()
        {
            _currentWaveIndex = Math.Min(_currentWaveIndex + 1, _battleConfig.WaveCount - 1);
            _spawnQueue.Clear();
            CurrentWave.MonstersToSpawn.ForEach(id => _spawnQueue.Enqueue(id));

            for (var index = 0; index < CurrentWave.MaxConcurrentMonsters; ++index)
            {
                SpawnNextMonster();
            }
        }

        private bool SpawnNextMonster()
        {
            if (!HasAnythingToSpawn)
            {
                return false;
            }

            var id = _spawnQueue.Dequeue();
            var cmd = new SpawnMonsterCommand(id, GetRandomSpawnerTransform());
            cmd.Execute();
            var monster = cmd.SpawnedMonster;
            monster.Init(_targetEntity);
            _currentlySpawnedMonsters.Add(monster);

            return true;
        }

        private Transform GetRandomSpawnerTransform()
        {
            var limit = _battleArena.Spawners.Length;

            var index = -1;
            do
            {
                index = UnityEngine.Random.Range(0, limit);
            }
            while (_lastlyUsedSpawners.Contains(index));

            if (_lastlyUsedSpawners.Count == CurrentWave.MaxConcurrentMonsters)
            {
                _lastlyUsedSpawners.Dequeue();
            }

            _lastlyUsedSpawners.Enqueue(index);

            return _battleArena.Spawners[index].SpawnerTransform;
        }

        private void SubscribeToEvents()
        {
            _messageService.Subscribe<GameEntityKilledMessage>(OnEntityKilled);
        }

        private void UnsubscribeFromEvents()
        {
            _messageService.Unsubscribe<GameEntityKilledMessage>(OnEntityKilled);
        }

        private void OnEntityKilled(GameEntityKilledMessage message)
        {
            var killedEntity = message.KilledEntity;
            var monster = _currentlySpawnedMonsters.FirstOrDefault(monster => monster.Entity == killedEntity);

            if (monster != null)
            {
                _currentlySpawnedMonsters.Remove(monster);
            }

            if (HasAnythingToSpawn)
            {
                SpawnNextMonster();
                return;
            }

            if (AreAnyMonstersSpawned)
            {
                return; //  Waiting for wave's end
            }

            if (IsCurrentWaveLast)
            {
                SucceedBattle();
            }
            else
            {
                StartNextWave();
            }
        }

        private void SetBattleState(BattleState newState)
        {
            var oldState = _battleState;
            _battleState = newState;
            _messageService.Send(new BattleStateChangedMessage(BattleId, oldState, newState));
        }

        private void SucceedBattle()
        {
            SetBattleState(BattleState.Succeed);
            _messageService.Send<WinGameMessage>();
            UnsubscribeFromEvents();
            _battleArena.OnBattleStop();
            ReleaseResources();
        }
    }
}