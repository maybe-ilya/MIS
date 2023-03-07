using mis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace mis.Drop
{
    [CreateAssetMenu(fileName = "DropServiceConfig", menuName = "mis Assets/DropServiceConfig")]
    internal sealed class DropServiceConfig :
        AbstractConfig,
        IDropServiceConfig,
        ISerializationCallbackReceiver
    {
        [Serializable]
        private struct EntityDropData
        {
            [SerializeField]
            public GameId EntityTypeId;
            [SerializeField]
            [CheckObject]
            public DropDataContainer DropData;
        }

        [SerializeField]
        private DropPointData _pointData;

        [SerializeField]
        [OneLine]
        private EntityDropData[] _dropData;

        private Dictionary<GameId, IDropData> _dropDataMap;

        public DropPointData PointData => _pointData;

        public bool TryGetDropData(GameId entityTypeId, out IDropData dropData) =>
            _dropDataMap.TryGetValue(entityTypeId, out dropData);

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            _dropDataMap = _dropData.ToDictionary(entry => entry.EntityTypeId, entry => (IDropData)entry.DropData);
        }
    }
}