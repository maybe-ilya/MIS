using mis.Core;
using UnityEngine;

namespace mis.Monsters
{
    public abstract class AbstractMonsterState : MonoBehaviour, IMonsterState
    {
        public virtual void OnEnterState() { }
        public virtual void OnExitState() { }
        public virtual void OnUpdateState() { }
    }

    public abstract class OwnedMonsterState<TMonster> : AbstractMonsterState
        where TMonster : class, IMonster
    {
        public TMonster Owner { get; private set; }

        public void SetOwner(TMonster owner)
        {
            Owner = owner;
            OnOwnerSet();
        }

        protected virtual void OnOwnerSet() { }
    }
}