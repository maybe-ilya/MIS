using mis.Core;

namespace mis.Messages
{
    public class PlayerKilledHandler : IMessageHandler
    {
        private readonly IMessageService _messageService;
        private const int PLAYER_INDEX = 0;
        private uint _pawnEntityIndex;

        public PlayerKilledHandler(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public void Subscribe()
        {
            _messageService.Subscribe<GameEntityKilledMessage>(OnEntityKilled);
            _messageService.Subscribe<PlayerControlledPawnMessage>(OnPawnControlled);
            _messageService.Subscribe<PlayerReleasedPawnMessage>(OnPawnReleased);
        }

        private void OnEntityKilled(GameEntityKilledMessage message)
        {
            if (_pawnEntityIndex != message.KilledEntity.Id)
            {
                return;
            }

            _messageService.Send<LoseGameMessage>();
            _pawnEntityIndex = 0;
        }

        private void OnPawnControlled(PlayerControlledPawnMessage message)
        {
            if (message.PlayerIndex != PLAYER_INDEX)
            {
                return;
            }

            _pawnEntityIndex = message.ControlledPawn.Entity.Id;
        }

        private void OnPawnReleased(PlayerReleasedPawnMessage message)
        {
            if (message.PlayerIndex != PLAYER_INDEX)
            {
                return;
            }

            _pawnEntityIndex = 0;
        }
    }
}