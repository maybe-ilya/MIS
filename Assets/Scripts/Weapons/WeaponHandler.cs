using UnityEngine;
using mis.Core;
using System;

namespace mis.Weapons
{
    public sealed class WeaponHandler : BaseWeaponHandler
    {
        [SerializeField]
        [CheckObject]
        private WeaponHandlerConfig _config;

        [SerializeField]
        [CheckObject]
        private Transform _weaponSocket;

        private IWeaponOwner _owner;
        private GameId _currentWeaponType, _lastWeaponType;
        private IWeapon _currentWeapon;

        public override IWeapon EqippedWeapon => _currentWeapon;

        public override GameId EquippedWeaponType => _currentWeaponType;

        public override bool IsEquippedAnyWeapon => _currentWeapon != null;

        public override event Action<IWeapon> OnWeaponChanged;

        private static IObjectService ObjectService => GameServices.Get<IObjectService>();

        public override void SetOwner(IWeaponOwner owner)
        {
            _owner = owner;
            _owner.ResourceContainer.OnAddResource += OnAddResourceToOwner;
        }

        public override void LoseOwner()
        {
            UnequipCurrentWeapon();
            _owner.ResourceContainer.OnAddResource -= OnAddResourceToOwner;
            _owner = null;
        }

        public override void EquipWeapon(GameId weaponId)
        {
            if (!_config.IsEquippableWeapon(weaponId)
                || weaponId == _currentWeaponType
                || !_owner.ResourceContainer.HasResource(weaponId))
            {
                return;
            }

            UnequipCurrentWeaponInternal(false);
            EquipWeaponInternal(weaponId, true);
        }

        public override void EquipLastWeapon()
        {
            EquipWeapon(_lastWeaponType);
        }

        public override void UnequipCurrentWeapon()
        {
            UnequipCurrentWeaponInternal(true);
        }

        public override void StartFire()
        {
            if (!IsEquippedAnyWeapon)
            {
                return;
            }
            _currentWeapon.StartFire();
        }

        public override void StopFire()
        {
            if (!IsEquippedAnyWeapon)
            {
                return;
            }
            _currentWeapon.StopFire();
        }

        public override void StartAltFire()
        {
            if (!IsEquippedAnyWeapon)
            {
                return;
            }
            _currentWeapon.StartAltFire();
        }

        public override void StopAltFire()
        {
            if (!IsEquippedAnyWeapon)
            {
                return;
            }
            _currentWeapon.StopAltFire();
        }

        private void OnDisable()
        {
            _currentWeapon = null;
            _lastWeaponType = 0;
            _currentWeaponType = 0;
        }

        private void EquipWeaponInternal(GameId weaponId, bool invokeChangeWeaponEvent = true)
        {
            _currentWeapon = ObjectService.SpawnEntityByType<IWeapon>(weaponId);
            _currentWeapon.Entity.transform.SetParent(_weaponSocket, false);
            _currentWeapon.OnEquip(_owner);
            _currentWeaponType = weaponId;

            if (invokeChangeWeaponEvent)
            {
                OnWeaponChanged?.Invoke(_currentWeapon);
            }
        }

        private void UnequipCurrentWeaponInternal(bool invokeChangeWeaponEvent = true)
        {
            if (_currentWeaponType == GameId.ZERO_ID)
            {
                return;
            }

            _currentWeapon.OnUnequip();
            ObjectService.DespawnEntity(_currentWeapon);
            _currentWeapon = null;
            _lastWeaponType = _currentWeaponType;

            if (invokeChangeWeaponEvent)
            {
                OnWeaponChanged?.Invoke(_currentWeapon);
            }
        }

        private void OnAddResourceToOwner(GameId resourceId, uint oldValue, uint newValue, uint delta)
        {
            if (!_config.IsEquippableWeapon(resourceId))
            {
                return;
            }

            if (oldValue == 0 && newValue == 1)
            {
                EquipWeapon(resourceId);
            }
        }
    }
}