using mis.Core;
using UnityEngine;

namespace mis.Drop
{
    internal sealed class DropResourcesCommand : AbstractCommand
    {
        private readonly IObjectService _objectService;
        private readonly IDropData _dropData;
        private readonly Vector3 _point;
        private readonly DropPointData _pointData;

        public DropResourcesCommand(
            IDropData dropData,
            IObjectService objectService,
            Vector3 point,
            DropPointData pointData)
        {
            _objectService = objectService;
            _dropData = dropData;
            _point = point;
            _pointData = pointData;
        }

        protected override void ExecuteInternal()
        {
            foreach (var resource in _dropData.Resources)
            {
                SpawnDrop(resource);
            }
        }

        private void SpawnDrop(Resource resource)
        {
            for (var index = 0; index < resource.Value; ++index)
            {
                var spawnPoint = _point + new Vector3(
                    x: _pointData.XRange.GetRandonValue(),
                    y: _pointData.YRange.GetRandonValue(),
                    z: _pointData.ZRange.GetRandonValue());
                var dropEntity = _objectService.SpawnEntityByType(resource.Id);

                dropEntity.transform.position = spawnPoint;
            }
        }
    }
}