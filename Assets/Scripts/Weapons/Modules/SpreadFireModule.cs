using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace mis.Weapons
{
    internal class SpreadFireModule : AbstractWeaponModule<SpreadFireModuleConfig>
    {
        [SerializeField]
        private UnityEvent _onShot;

        private bool _isShooting;

        private float _cooldown;

        public void StartShooting()
        {
            _isShooting = true;
        }

        public void StopShooting()
        {
            _isShooting = false;
        }

        private void Update()
        {
            if (_cooldown > float.Epsilon)
            {
                _cooldown -= Time.deltaTime;
                return;
            }

            if (!_isShooting || !HasEnoughAmmo)
            {
                return;
            }

            SpendAmmo();
            _onShot?.Invoke();

            var damagePoints = GetShotPoints(Config.ShotsCount);
            var hitPoints = new List<RaycastHit>();

            for (var index = 0; index < damagePoints.Length; index++)
            {
                var damagePoint = damagePoints[index];
                var projectile = GetProjectile(Config.ProjectileId);
                projectile.Setup(ShootPoint, damagePoint);

                if (RaycastFromViewPoint(damagePoint, out var hit))
                {
                    hitPoints.Add(hit);
                }
            }

            DamageService.ApplyPointDamage(DamageInstigator, Config.DamageData, ShootPoint, hitPoints);

            _cooldown = Config.ShotCooldown;
        }
    }
}