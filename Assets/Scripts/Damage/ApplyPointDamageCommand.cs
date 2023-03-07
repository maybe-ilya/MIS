using UnityEngine;
using mis.Core;
using System.Collections.Generic;

namespace mis.Damage
{
    internal sealed class ApplyPointDamageCommand : AbstractDamageCommand
    {
        private readonly IList<RaycastHit> _hitPoints;
        private readonly Dictionary<GameEntity, uint> _calculatedDamage;

        public ApplyPointDamageCommand(
            GameEntity instigator,
            DamageData damageData,
            Vector3 shootPoint,
            IList<RaycastHit> hitPoints,
            IDamageConfig damageConfig,
            IMessageService messageService)
            : base(instigator, shootPoint, damageData, damageConfig, messageService)
        {
            _hitPoints = hitPoints;
            _calculatedDamage = new Dictionary<GameEntity, uint>();
        }

        protected override void ExecuteInternal()
        {
            CalculateDamage();
            ApplyDamage();
            SucceedCommand();
        }

        private void CalculateDamage()
        {
            foreach (var hitPoint in _hitPoints)
            {
                if (!TryGetDamageTo(hitPoint, out var entity, out var damage))
                {
                    continue;
                }

                if (!_calculatedDamage.ContainsKey(entity))
                {
                    _calculatedDamage[entity] = damage;
                }
                else
                {
                    _calculatedDamage[entity] += damage;
                }
            }
        }

        private void ApplyDamage()
        {
            foreach ((var damagedEntity, var damage) in _calculatedDamage)
            {
                ApplyDamageTo(damagedEntity, damage);
            }
        }
    }
}