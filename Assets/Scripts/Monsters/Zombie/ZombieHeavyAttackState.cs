using Cysharp.Threading.Tasks;
using mis.Core;
using System.Threading;
using UnityEngine;

namespace mis.Monsters
{
    public sealed class ZombieHeavyAttackState : ZombieState
    {
        [SerializeField]
        private Transform _attackTracePoint;
        [SerializeField]
        private Vector3 _attackTraceExtend;
        [SerializeField]
        private float _attackTraceDistance;
        [SerializeField]
        private float _attackDuration;
        [SerializeField]
        private DamageData _damageData;
        [SerializeField]
        private Color _gizmoColor;
        [SerializeField]
        private AnimatorHash _animTrigParam;

        private CancellationTokenSource _cancelTokenSource;

        public override void OnEnterState()
        {
            Owner.StopNavMeshAgent();
            Owner.Animator.SetTrigger(_animTrigParam);
            WaitToContinueChasingAsync().Forget();
        }

        private async UniTaskVoid WaitToContinueChasingAsync()
        {
            CancelWait();
            _cancelTokenSource = new CancellationTokenSource();
            await UniTaskUtils.DelayForSeconds(_attackDuration, _cancelTokenSource);
            Owner.ChangeState(Owner.ChaseState);
            _cancelTokenSource = null;
        }

        // Used by animation event
        public void OnHeavyAttackAnimEvent()
        {
            if (Physics.BoxCast(
                _attackTracePoint.position,
                _attackTraceExtend,
                _attackTracePoint.forward,
                out var hit,
                _attackTracePoint.rotation,
                _attackTraceDistance,
                Owner.AttackMask))
            {
                GameServices.Get<IDamageService>().ApplyPointDamage(Owner.Entity, _damageData, _attackTracePoint.position, new[] { hit });
            }
        }

        private void CancelWait()
        {
            _cancelTokenSource?.Cancel();
            _cancelTokenSource = null;
        }

        private void OnDisable() => CancelWait();

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (_attackTracePoint == null)
            {
                return;
            }

            using (new GizmosMatrixScope(_attackTracePoint.localToWorldMatrix))
            using (new GizmosColorScope(_gizmoColor))
            {
                Gizmos.DrawLine(Vector3.zero, Vector3.forward * _attackTraceDistance);
                Gizmos.DrawWireCube(Vector3.zero, _attackTraceExtend * 2);
                Gizmos.DrawWireCube(Vector3.forward * _attackTraceDistance, _attackTraceExtend * 2);
            }
        }
#endif
    }
}