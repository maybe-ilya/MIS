using mis.Core;
using UnityEngine;

namespace mis.UI
{
    internal class CombatHUD : AbstractHUD
    {
        [SerializeField]
        [CheckObject]
        private ResourceIndicatorGroup _healthIndicator, _armorIndicator, _ammonIndicator;

        private IPlayerPawn _pawn;
        private BaseWeaponHandler _weaponHandler;

        private IPlayerService _playerService;
        private IMessageService _messageService;

        protected override void OnOpened()
        {
            _playerService = GameServices.Get<IPlayerService>();
            _messageService = GameServices.Get<IMessageService>();
            _messageService.Subscribe<LevelStartedMessage>(OnLevelStartedMessage);
        }

        protected override void OnClosed()
        {
            _messageService.Unsubscribe<LevelStartedMessage>(OnLevelStartedMessage);

            _playerService = null;
            _messageService = null;

            _healthIndicator.Deinit();
            _armorIndicator.Deinit();
            _ammonIndicator.Deinit();

            _weaponHandler = null;
            _pawn = null;
        }

        private void OnLevelStartedMessage(LevelStartedMessage message)
        {
            var pawn = _playerService.GetFirstPlayerController().Pawn;
            if (pawn == null)
            {
                return;
            }

            if (!pawn.Entity.TryGetComponent<BaseResourceContainer>(out var resourceContainer)
                || !pawn.Entity.TryGetComponent<BaseWeaponHandler>(out var weaponHandler))
            {
                return;
            }

            _pawn = pawn;
            _weaponHandler = weaponHandler;
            _weaponHandler.OnWeaponChanged += OnWeaponChanged;

            SetAmmoIndicator(_weaponHandler.EqippedWeapon);

            _healthIndicator.Init(resourceContainer);
            _armorIndicator.Init(resourceContainer);
            _ammonIndicator.Init(resourceContainer);
        }

        private void SetAmmoIndicator(IWeapon weapon)
        {
            var hasWeapon = weapon != null;
            _ammonIndicator.SetResourceId(hasWeapon ? weapon.AmmoType : GameId.ZERO_ID);
            _ammonIndicator.SetVisible(hasWeapon);
        }

        private void OnWeaponChanged(IWeapon weapon) =>
            SetAmmoIndicator(_weaponHandler.EqippedWeapon);
    }
}