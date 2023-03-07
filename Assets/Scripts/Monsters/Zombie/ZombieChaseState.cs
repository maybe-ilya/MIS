using mis.Core;
using UnityEngine;

namespace mis.Monsters
{
    public sealed class ZombieChaseState : ZombieState
    {
        [SerializeField]
        private float _checkAttackDistance;
        [SerializeField]
        private float _checkAttackDot;
        [SerializeField]
        private Color _gizmoColor;

        public override void OnEnterState()
        {
            Owner.SetDestinationToTarget();
        }

        public override void OnUpdateState()
        {
            var navmeshAgent = Owner.NavMeshAgent;
            var thisTransform = Owner.transform;
            var targetPosition = navmeshAgent.destination;
            var thisPosition = thisTransform.position;

            Owner.UpdateAnimationSpeed();

            var targetDot = Vector3.Dot(thisTransform.forward, (targetPosition - thisPosition).normalized);

            if (targetDot >= _checkAttackDot && Vector3.Distance(targetPosition, thisPosition) < _checkAttackDistance)
            {
                Owner.ChangeState(RandomUtils.GetRandomBool() ? Owner.HeavyAttackState : Owner.LightAttackState);
            }
            else
            {
                Owner.SetDestinationToTarget();
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            using (new GizmosMatrixScope(transform.localToWorldMatrix))
            using (new GizmosColorScope(Color.red))
            {
                Gizmos.DrawLine(Vector3.zero, Vector3.forward * _checkAttackDistance);
            }
        }
#endif
    }
}