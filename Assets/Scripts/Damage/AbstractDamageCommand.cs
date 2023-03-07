using System;
using UnityEngine;
using mis.Core;

namespace mis.Damage
{
    internal abstract class AbstractDamageCommand : AbstractCommand
    {
        protected readonly GameEntity _instigator;
        protected readonly Vector3 _shootPoint;
        protected readonly DamageData _damageData;
        protected readonly IDamageConfig _damageConfig;
        protected readonly IMessageService _messageService;

        public AbstractDamageCommand(
            GameEntity instigator,
            Vector3 shootPoint,
            DamageData damageData,
            IDamageConfig damageConfig,
            IMessageService messageService)
        {
            _instigator = instigator;
            _shootPoint = shootPoint;
            _damageData = damageData;
            _damageConfig = damageConfig;
            _messageService = messageService;
        }

        protected bool TryGetDamageTo(RaycastHit hitPoint, out GameEntity entity, out uint damage)
        {
            damage = 0;

            var hitGameObject = hitPoint.transform.gameObject;
            if (!hitGameObject.TryGetComponent<GameEntity>(out entity) ||
                !hitGameObject.TryGetComponent<BaseResourceContainer>(out var container))
            {
                return false;
            }

            if (_damageData.IsDistanceDependentDamage)
            {
                var hitVector = hitPoint.point - _shootPoint;
                var distance = hitVector.magnitude;
                var ratio = _damageData.DamageDistanceCurve.Evaluate(distance / _damageData.MaxDistance);
                damage = _damageData.DamageRange.Lerp(ratio);
            }
            else
            {
                damage = _damageData.DamageRange.Max;
            }

            return true;
        }

        protected void ApplyDamageTo(GameEntity damagedEntity, uint damage)
        {
            var resourceContainer = damagedEntity.GetComponent<BaseResourceContainer>();
            var applyDamage = damage;

            if (resourceContainer.HasResource(GameIds.RESOURCE_ARMOR))
            {
                var armorDamage = (uint)(applyDamage * _damageConfig.ArmorDefenceRatio);
                applyDamage -= armorDamage;
                armorDamage = (uint)(armorDamage * _damageConfig.ArmorDefencePower);

                resourceContainer.RemoveResource(GameIds.RESOURCE_ARMOR, armorDamage);
            }

            resourceContainer.RemoveResource(GameIds.RESOURCE_HEALTH, applyDamage);

            if (!resourceContainer.HasResource(GameIds.RESOURCE_HEALTH))
            {
                _messageService.Send(new GameEntityKilledMessage(damagedEntity));
            }
        }
    }
}