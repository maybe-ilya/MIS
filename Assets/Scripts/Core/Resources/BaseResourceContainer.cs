using UnityEngine;

namespace mis.Core
{
    public delegate void ResourceChangeEvent(GameId resourceId, uint oldValue, uint newValue, uint delta);

    public abstract class BaseResourceContainer : MonoBehaviour
    {
        public abstract event ResourceChangeEvent OnAddResource;
        public abstract event ResourceChangeEvent OnRemoveResource;
        public abstract void AddLimit(ResourceLimit limitToAdd);
        public abstract void AddLimit(GameId id, UintRange limitToAdd);
        public abstract bool AddResource(Resource resourceToAdd);
        public abstract bool AddResource(GameId id, uint valueToAdd);
        public abstract void ClearAllLimitData();
        public abstract void ClearAllResourceData();
        public abstract void ClearResource(GameId resourceId);
        public abstract Resource[] GetAllResources();
        public abstract ResourceLimit GetLimit(GameId id);
        public abstract Resource GetResource(GameId id);
        public abstract float GetResourceRatio(GameId id);
        public abstract float GetResourceRatio(GameId id, uint value);
        public abstract bool HasLimit(GameId id);
        public abstract bool HasResource(GameId id);
        public abstract void RefillResource(GameId resourceId);
        public abstract void RemoveLimit(ResourceLimit limitToRemove);
        public abstract void RemoveLimit(GameId id);
        public abstract bool RemoveResource(Resource resourceToSubtract);
        public abstract bool RemoveResource(GameId id, uint valueToSubtract);
        public abstract bool TryGetResource(GameId id, out Resource resource);
    }
}