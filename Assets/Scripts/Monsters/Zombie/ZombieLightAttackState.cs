using Cysharp.Threading.Tasks;
using mis.Core;
using System.Threading;
using UnityEngine;

namespace mis.Monsters
{
    public sealed class ZombieLightAttackState : ZombieState
    {
        [SerializeField]
        private Transform _attackTracePoint;
        [SerializeField]
        private float _attackTraceRadius;
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
        [SerializeField]
        private int _animBlendLayer;

        private CancellationTokenSource _cancelTokenSource;

        public override void OnEnterState()
        {
            Owner.Animator.SetLayerWeight(_animBlendLayer, 1.0f);
            Owner.Animator.SetTrigger(_animTrigParam);
            WaitToContinueChasingAsync().Forget();
        }

        public override void OnExitState()
        {
            Owner.Animator.SetLayerWeight(_animBlendLayer, 0.0f);
        }

        public override void OnUpdateState()
        {
            Owner.UpdateAnimationSpeed();
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
        public void OnLightAttackAnimEvent()
        {
            if (Physics.SphereCast(
                _attackTracePoint.position,
                _attackTraceRadius,
                _attackTracePoint.forward,
                out var hit,
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
                Gizmos.DrawWireSphere(Vector3.zero, _attackTraceRadius);
                Gizmos.DrawWireSphere(Vector3.forward * _attackTraceDistance, _attackTraceRadius);
            }
        }
#endif
    }
}