using mis.Core;
using UnityEngine;
using UnityEngine.AI;

namespace mis.Monsters
{
    public abstract class AbstractMonster<TState> : AbstractGameEntityComponent, IMonster
        where TState : class, IMonsterState
    {
        [SerializeField]
        private NavMeshAgent _navMeshAgent;
        [SerializeField]
        private BaseResourceContainer _resourceContainer;

        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public BaseResourceContainer ResourceContainer => _resourceContainer;

        public GameEntity Target { get; private set; }
        public TState CurrentState { get; private set; }

        public void Init(GameEntity target)
        {
            Target = target;
            OnInit();
        }

        public void Deinit()
        {
            OnDeinit();
            Target = null;
        }

        protected virtual void OnInit()
        {
            SetNavMeshAgentEnabled(true);
            ResourceContainer.OnAddResource += OnAddResource;
            ResourceContainer.OnRemoveResource += OnRemoveResource;
            ChangeState(GetInitialState());
        }

        protected virtual void OnDeinit()
        {
            SetNavMeshAgentEnabled(false);
            ResourceContainer.OnAddResource -= OnAddResource;
            ResourceContainer.OnRemoveResource -= OnRemoveResource;
        }

        protected void Despawn()
        {
            GameServices.Get<IObjectService>().DespawnEntity(this);
        }

        protected abstract TState GetInitialState();

        public void ChangeState(TState state)
        {
            CurrentState?.OnExitState();
            CurrentState = state;
            CurrentState?.OnEnterState();
        }

        private void Update()
        {
            CurrentState.OnUpdateState();
        }

        private void OnDisable()
        {
            ChangeState(null);
        }

        private void OnAddResource(GameId resourceId, uint oldValue, uint newValue, uint delta) =>
            OnAddResource_Implementation(resourceId, oldValue, newValue, delta);

        protected virtual void OnAddResource_Implementation(GameId resourceId, uint oldValue, uint newValue, uint delta) { }

        private void OnRemoveResource(GameId resourceId, uint oldValue, uint newValue, uint delta) =>
            OnRemoveResource_Implementation(resourceId, oldValue, newValue, delta);

        protected virtual void OnRemoveResource_Implementation(GameId resourceId, uint oldValue, uint newValue, uint delta) { }

        public void SetDestinationToTarget()
        {
            if (!gameObject.activeInHierarchy || !NavMeshAgent.enabled)
            {
                return;
            }
            NavMeshAgent.SetDestination(Target.transform.position);
        }

        public void StopNavMeshAgent()
        {
            NavMeshAgent.velocity = Vector3.zero;
        }

        public void SetNavMeshAgentEnabled(bool enable)
        {
            NavMeshAgent.enabled = enable;
        }
    }
}