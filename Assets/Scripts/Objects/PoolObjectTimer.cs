using mis.Core;
using System.Collections;
using UnityEngine;

namespace mis.Objects
{
    public sealed class PoolObjectTimer : AbstractGameEntityComponent
    {
        [SerializeField]
        private float _time;

        private Coroutine _timerRoutine;

        private static IMessageService MessageService =>
            GameServices.Get<IMessageService>();

        private static IObjectService ObjectService =>
            GameServices.Get<IObjectService>();

        private void OnEnable()
        {
            Initialize();
        }

        private void OnDisable()
        {
            Deinitialize();
        }

        private void Initialize()
        {
            MessageService.Subscribe<SwitchLevelMessage>(OnLevelSwitch);
            StopTimerIfNeeded();
            _timerRoutine = StartCoroutine(WaitAndReturnToPool());
        }

        private void Deinitialize()
        {
            StopTimerIfNeeded();
            MessageService.Unsubscribe<SwitchLevelMessage>(OnLevelSwitch);
        }

        private IEnumerator WaitAndReturnToPool()
        {
            yield return new WaitForSeconds(_time);
            Despawn();
        }

        private void StopTimerIfNeeded()
        {
            if (_timerRoutine != null)
            {
                StopCoroutine(_timerRoutine);
            }
            _timerRoutine = null;
        }

        private void Despawn()
        {
            ObjectService.DespawnEntity(this);
        }

        private void OnLevelSwitch(SwitchLevelMessage obj)
        {
            Despawn();
        }
    }
}