using mis.Core;
using UnityEngine;

namespace mis.UI
{
    internal abstract class AbstractResourceIndicator : MonoBehaviour
    {
        [SerializeField]
        protected GameId _resourceId;

        protected BaseResourceContainer ResourceContainer { get; private set; }

        protected bool HasContainer => ResourceContainer != null;

        public void Init(BaseResourceContainer resourceContainer)
        {
            ResourceContainer = resourceContainer;
            Subscribe();
            OnInit();
        }

        public void Deinit()
        {
            Unsubscribe();
            ResourceContainer = null;
        }

        public void SetResourceId(GameId resourceId)
        {
            _resourceId = resourceId;

            if (_resourceId == GameId.ZERO_ID)
            {
                return;
            }
            OnInit();
        }

        public void ClearResourceID() =>
            SetResourceId(GameId.ZERO_ID);

        private void Subscribe()
        {
            if (!HasContainer)
            {
                return;
            }

            ResourceContainer.OnAddResource += OnAddResource;
            ResourceContainer.OnRemoveResource += OnRemoveResource;
        }

        private void Unsubscribe()
        {
            if (!HasContainer)
            {
                return;
            }

            ResourceContainer.OnAddResource -= OnAddResource;
            ResourceContainer.OnRemoveResource -= OnRemoveResource;
        }

        private void OnAddResource(GameId changedResourceId, uint oldValue, uint newValue, uint delta)
        {
            if (_resourceId == GameId.ZERO_ID || changedResourceId != _resourceId)
            {
                return;
            }
            OnResourceAdded(oldValue, newValue, delta);
        }

        private void OnRemoveResource(GameId changedResourceId, uint oldValue, uint newValue, uint delta)
        {
            if (_resourceId == GameId.ZERO_ID || changedResourceId != _resourceId)
            {
                return;
            }
            OnResourceRemoved(oldValue, newValue, delta);
        }

        protected abstract void OnInit();
        protected abstract void OnResourceAdded(uint oldValue, uint newValue, uint delta);
        protected abstract void OnResourceRemoved(uint oldValue, uint newValue, uint delta);
    }
}