using UnityEngine;

namespace mis.Weapons
{
    internal class ConstantFireModule : AbstractWeaponModule<ConstantFireModuleConfig>
    {
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

            var damagePoint = GetShotPoint();
            var projectile = GetProjectile(Config.ProjectileId);
            projectile.Setup(ShootPoint, damagePoint);

            if (RaycastFromViewPoint(damagePoint, out var hit))
            {
                DamageService.ApplyPointDamage(DamageInstigator, Config.DamageData, ShootPoint, new[] { hit });
            }

            _cooldown = Config.ShotCooldown;
        }
    }
}