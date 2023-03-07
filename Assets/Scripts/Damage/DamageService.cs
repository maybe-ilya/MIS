using System;
using UnityEngine;
using mis.Core;
using System.Collections.Generic;

namespace mis.Damage
{
    public sealed class DamageService : IDamageService, IStartableService
    {
        private readonly IConfigService _configService;
        private readonly IMessageService _messageService;
        private IDamageConfig _globalDamageConfig;

        public int StartPriority => 0;

        public DamageService(IConfigService configService, IMessageService messageService)
        {
            _configService = configService;
            _messageService = messageService;
        }

        public void OnServiceStart()
        {
            _globalDamageConfig = _configService.GetConfig<IDamageConfig>(GameIds.GLOBAL_DAMAGE_CFG);
        }

        public void ApplyPointDamage(GameEntity instigator, DamageData damageData, Vector3 shootPoint, IList<RaycastHit> hitPoints) =>
            new ApplyPointDamageCommand(instigator, damageData, shootPoint, hitPoints, _globalDamageConfig, _messageService).Execute();
    }
}