using Cysharp.Threading.Tasks;
using mis.Core;
using System.Threading;
using UnityEngine;

namespace mis.Monsters
{
    public sealed class ZombieDeathState : ZombieState
    {
        [SerializeField]
        private AnimatorHash _deathTrigHash;
        [SerializeField]
        private float _stateTime;
        [SerializeField]
        [CheckObject]
        private AbstractSoundCue _deathCue;

        private CancellationTokenSource _cancelTokenSource;

        public override void OnEnterState()
        {
            Owner.SoundCuePlayer.Play(_deathCue);
            Owner.SetNavMeshAgentEnabled(false);
            Owner.SetCollisionEnabled(false);
            Owner.Animator.SetTrigger(_deathTrigHash);
            WaitToSetDeadAsync().Forget();
        }

        private async UniTaskVoid WaitToSetDeadAsync()
        {
            CancelWait();
            await UniTaskUtils.DelayForSeconds(_stateTime, _cancelTokenSource = new CancellationTokenSource());
            Owner.SetDead();
        }

        private void CancelWait()
        {
            _cancelTokenSource?.Cancel();
            _cancelTokenSource = null;
        }
    }
}