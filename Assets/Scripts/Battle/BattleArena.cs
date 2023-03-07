using mis.Core;
using UnityEngine;

namespace mis.Battle
{
    internal sealed class BattleArena : MonoBehaviour, IBattleArena
    {
        [SerializeField]
        private TetherAreaVisibilityChecker _visibilityChecker;
        [SerializeField]
        private MonsterSpawner[] _monsterSpawners;

        public ITetherAreaInfoProvider TetherAreaInfo => _visibilityChecker;

        public IMonsterSpawner[] Spawners => _monsterSpawners;

        public void OnBattleStart()
        {
            _visibilityChecker.OnBattleStart();
        }

        public void OnBattleStop()
        {
            _visibilityChecker.OnBattleStop();
        }

#if UNITY_EDITOR
        [ContextMenu(nameof(GatherSpawners))]
        private void GatherSpawners()
        {
            _monsterSpawners = GetComponentsInChildren<MonsterSpawner>();
        }
#endif
    }
}