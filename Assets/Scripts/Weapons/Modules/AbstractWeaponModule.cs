using mis.Core;
using UnityEngine;
using URandom = UnityEngine.Random;

namespace mis.Weapons
{
    internal class AbstractWeaponModule<T> : MonoBehaviour where T : AbstractWeaponModuleConfig
    {
        [SerializeField]
        private T _config;
        [SerializeField]
        protected Transform _shootTransform;

        protected T Config => _config;

        protected static IObjectService ObjectService =>
            GameServices.Get<IObjectService>();

        protected static IDamageService DamageService =>
            GameServices.Get<IDamageService>();

        protected IWeaponModuleOwner ModuleOwner { get; private set; }

        protected GameEntity DamageInstigator =>
            ModuleOwner.WeaponOwner.Entity;

        protected IViewPoint ViewPoint =>
            ModuleOwner.WeaponOwner.ViewPoint;

        protected Vector3 ViewPointPosition =>
            ViewPoint.Position;

        protected BaseResourceContainer ResourceContainer =>
            ModuleOwner.WeaponOwner.ResourceContainer;

        protected bool HasEnoughAmmo =>
            ResourceContainer.GetResource(ModuleOwner.AmmoType).Value >= Config.AmmoPerShot;

        protected Vector3 ShootPoint =>
            _shootTransform.position;

        public void SetOwner(IWeaponModuleOwner owner) =>
            ModuleOwner = owner;

        protected Vector3 GetShotPoint() =>
            ViewPointPosition
            + ViewPoint.ForwardDirection * Config.Distance
            + ViewPoint.RightDirection * URandom.Range(Config.HorizontalSpread.Min, Config.HorizontalSpread.Max)
            + ViewPoint.UpDirection * URandom.Range(Config.VerticalSpread.Min, Config.VerticalSpread.Max);

        protected Vector3[] GetShotPoints(uint count)
        {
            var result = new Vector3[count];
            for (var index = 0; index < count; ++index)
            {
                result[index] = GetShotPoint();
            }
            return result;
        }

        protected bool RaycastFromViewPoint(Vector3 target, out RaycastHit hit)
        {
            var basePoint = ViewPointPosition;
            return Physics.Raycast(basePoint, (target - basePoint).normalized, out hit, Config.Distance, Config.CollisionMask);
        }

        protected void SpendAmmo()
        {
            ResourceContainer.RemoveResource(ModuleOwner.AmmoType, Config.AmmoPerShot);
        }

        protected IProjectile GetProjectile(GameId id) =>
            ObjectService.SpawnEntityByType<IProjectile>(id);
    }
}