using System.Collections.Generic;
using mis.Core;
using System.Linq;

namespace mis.Resources
{
    public sealed class ResourceContainer : BaseResourceContainer
    {
        private readonly Dictionary<GameId, Resource> _resources = new();
        private readonly Dictionary<GameId, ResourceLimit> _resourceLimits = new();

        public override event ResourceChangeEvent OnAddResource;
        public override event ResourceChangeEvent OnRemoveResource;

        public override Resource GetResource(GameId id) =>
            _resources.TryGetValue(id, out var resource)
                ? resource
                : GetMinResource(id);

        public override Resource[] GetAllResources() =>
            _resources.Values.ToArray();

        public override bool TryGetResource(GameId id, out Resource resource) =>
            _resources.TryGetValue(id, out resource);

        public override bool HasResource(GameId id) =>
            _resources.TryGetValue(id, out var resource) && resource.Value > 0;

        public override bool AddResource(Resource resourceToAdd)
        {
            var id = resourceToAdd.Id;
            var limit = GetLimit(id);
            var resource = GetResource(id);

            if (resource.Value >= limit.Range.Max)
            {
                return false;
            }

            var resultResource = _resources[id] = limit.LimitResource(resource + resourceToAdd);
            InvokeOnAddResource(resource, resultResource);

            return true;
        }

        public override bool AddResource(GameId id, uint valueToAdd) =>
            AddResource((id, valueToAdd));

        public override bool RemoveResource(Resource resourceToSubtract)
        {
            var id = resourceToSubtract.Id;
            var resource = GetResource(id);
            var limit = GetLimit(id);

            if (resource.Value <= limit.Range.Min)
            {
                return false;
            }

            var resultResource = _resources[id] = limit.LimitResource(resource - resourceToSubtract);
            InvokeOnRemoveResource(resource, resultResource);

            return true;
        }

        public override bool RemoveResource(GameId id, uint valueToSubtract) =>
            RemoveResource((id, valueToSubtract));

        public override void ClearResource(GameId resourceId) =>
            RemoveResource(GetMaxResource(resourceId));

        public override void RefillResource(GameId resourceId) =>
            AddResource(GetMaxResource(resourceId));

        public override void ClearAllResourceData() =>
            _resources.Clear();

        public override void AddLimit(ResourceLimit limitToAdd) =>
            _resourceLimits[limitToAdd.Id] = limitToAdd;

        public override void AddLimit(GameId id, UintRange limitToAdd) =>
            _resourceLimits[id] = (id, limitToAdd);

        public override void RemoveLimit(ResourceLimit limitToRemove) =>
            _resourceLimits.Remove(limitToRemove.Id);

        public override void RemoveLimit(GameId id) =>
            _resourceLimits.Remove(id);

        public override ResourceLimit GetLimit(GameId id)
        {
            if (!_resourceLimits.TryGetValue(id, out var foundLimit))
            {
                _resourceLimits[id] = foundLimit = new ResourceLimit(id, (uint.MinValue, uint.MaxValue));
            }
            return foundLimit;
        }

        public override bool HasLimit(GameId id) =>
            _resourceLimits.ContainsKey(id);

        public override void ClearAllLimitData() =>
            _resourceLimits.Clear();

        public override float GetResourceRatio(GameId id)
        {
            if (!TryGetResource(id, out var resource))
            {
                return 0f;
            }

            return GetResourceRatio(id, resource.Value);
        }

        public override float GetResourceRatio(GameId id, uint value)
        {
            var limit = GetLimit(id);

            var delimiter = value - limit.Range.Min;
            var divider = limit.Range.Max - limit.Range.Min;

            return (float)delimiter / (float)divider;
        }

        private Resource GetMinResource(GameId id) => new(id, GetLimit(id).Range.Min);

        private Resource GetMaxResource(GameId id) => new(id, GetLimit(id).Range.Max);

        private void InvokeOnAddResource(Resource oldResource, Resource newResource) =>
            OnAddResource?.Invoke(
                oldResource.Id,
                oldResource.Value,
                newResource.Value,
                newResource.Value - oldResource.Value);

        private void InvokeOnRemoveResource(Resource oldResource, Resource newResource) =>
            OnRemoveResource?.Invoke(
                oldResource.Id,
                oldResource.Value,
                newResource.Value,
                oldResource.Value - newResource.Value);
    }
}