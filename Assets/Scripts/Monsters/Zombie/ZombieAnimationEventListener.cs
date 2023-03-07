using UnityEngine;
using UnityEngine.Events;

namespace mis.Monsters
{
    internal sealed class ZombieAnimationEventListener : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _onLightAttack;

        [SerializeField]
        private UnityEvent _onHeavyAttack;

        //	Used by animation event
        public void OnLightAtack() => _onLightAttack?.Invoke();
        public void OnHeavyAttack() => _onHeavyAttack?.Invoke();
    }
}