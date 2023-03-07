using mis.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace mis.Monsters
{
    [SelectionBase]
    public sealed class Zombie : AbstractMonster<ZombieState>
    {
        [Header("Shared Data")]
        [SerializeField]
        [CheckObject]
        private Animator _animator;

        [SerializeField]
        private AnimatorHash _speedFloatParam;

        [SerializeField]
        private LayerMask _attackMask;

        [SerializeField]
        [CheckObject]
        private Collider _collider;

        [SerializeField]
        [CheckObject]
        private AbstractSoundCuePlayer _soundCuePlayer;

        [SerializeField]
        [CheckObject]
        private AbstractSoundCue _defaultCue;

        [Header("States")]
        [SerializeField]
        [CheckObject]
        private ZombieState _idleState;

        [SerializeField]
        [CheckObject]
        private ZombieState _chaseState;

        [SerializeField]
        [CheckObject]
        private ZombieState _lightAttackState;

        [SerializeField]
        [CheckObject]
        private ZombieState _heavyAttackState;

        [SerializeField]
        [CheckObject]
        private ZombieState _deathState;

        public Animator Animator => _animator;
        public LayerMask AttackMask => _attackMask;
        public AbstractSoundCuePlayer SoundCuePlayer => _soundCuePlayer;

        public ZombieState IdleState => _idleState;
        public ZombieState ChaseState => _chaseState;
        public ZombieState LightAttackState => _lightAttackState;
        public ZombieState HeavyAttackState => _heavyAttackState;
        public ZombieState DeathState => _deathState;

        protected override ZombieState GetInitialState() =>
            ChaseState;

        protected override void OnInit()
        {
            SetCollisionEnabled(true);
            SoundCuePlayer.ApplyCue(_defaultCue);
            IdleState.SetOwner(this);
            ChaseState.SetOwner(this);
            LightAttackState.SetOwner(this);
            HeavyAttackState.SetOwner(this);
            DeathState.SetOwner(this);

            base.OnInit();
        }

        protected override void OnDeinit()
        {
            SoundCuePlayer.Stop();
            base.OnDeinit();
        }

        public void UpdateAnimationSpeed()
        {
            Animator.SetFloat(_speedFloatParam, NavMeshAgent.velocity.magnitude);
        }

        public void SetCollisionEnabled(bool enabled)
        {
            _collider.enabled = enabled;
        }

        public void SetDead()
        {
            Deinit();
            Despawn();
        }

        protected override void OnRemoveResource_Implementation(GameId resourceId, uint oldValue, uint newValue, uint delta)
        {
            if (resourceId == GameIds.RESOURCE_HEALTH && newValue == 0 && oldValue != 0)
            {
                ChangeState(DeathState);
            }
            else
            {
                SoundCuePlayer.Play();
            }
        }
    }
}