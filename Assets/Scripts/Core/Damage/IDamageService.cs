using System;
using UnityEngine;
using mis.Core;
using System.Collections.Generic;

namespace mis.Core
{
    public interface IDamageService : IService
    {
        void ApplyPointDamage(GameEntity instigator, DamageData damageData, Vector3 shootPoint, IList<RaycastHit> hitPoints);
    }
}