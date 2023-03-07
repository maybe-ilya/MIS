using mis.Core;
using UnityEngine;

namespace mis.Battle
{
    internal sealed class BattleTriggerInvoker : MonoBehaviour
    {
        [SerializeField]
        private GameId _battleId;
        [SerializeField]
        private BattleArena _battleAreaInfo;

        private void OnTriggerEnter(Collider other)
        {
            if (_battleId == GameId.ZERO_ID)
            {
                return;
            }

            new StartBattleCommand(_battleId, _battleAreaInfo).Execute();
        }
    }
}