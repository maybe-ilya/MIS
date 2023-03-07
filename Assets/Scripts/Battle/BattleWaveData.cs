using mis.Core;
using System;
using UnityEngine;

namespace mis.Battle
{
    [Serializable]
    public sealed class BattleWaveData : IBattleWaveData
    {
        [SerializeField]
        private int _maxConcurrentMonsters;

        [SerializeField]
        private GameId[] _monsters;

        public int MaxConcurrentMonsters => _maxConcurrentMonsters;

        public GameId[] MonstersToSpawn => _monsters;
    }
}