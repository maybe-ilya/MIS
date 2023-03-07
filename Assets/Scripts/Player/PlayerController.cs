using UnityEngine;
using mis.Core;

namespace mis.Player
{
    public sealed class PlayerController : MonoBehaviour, IPlayerController
    {
        private IPlayer _player;
        private InputController _inputController;
        private IMessageService _messageService;

        public IPlayerPawn Pawn { get; private set; }

        public int PlayerIndex => _player.Index;

        public void Setup(IPlayer player)
        {
            _player = player;
            _inputController = GetComponent<InputController>();
            _messageService = GameServices.Get<IMessageService>();
        }

        public void ControlPawn(IPlayerPawn pawn)
        {
            if (Pawn != null)
            {
                ReleasePawn();
            }

            Pawn = pawn;
            _inputController.SetPawn(pawn);
            Pawn.OnGainControl();
            _messageService.Send(new PlayerControlledPawnMessage(PlayerIndex, Pawn));
        }

        public void ReleasePawn()
        {
            var releasedPawn = Pawn;
            Pawn = null;

            releasedPawn.OnLoseControl();
            _messageService.Send(new PlayerControlledPawnMessage(PlayerIndex, releasedPawn));
        }

        public void SetPawnInputMode() =>
            _inputController.SetPawnInputMode();

        public void SetUIInputMode() =>
            _inputController.SetUIInputMode();
    }
}
