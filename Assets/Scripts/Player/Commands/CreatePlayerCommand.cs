using mis.Core;

namespace mis.Player
{
    public class CreatePlayerCommand : AbstractCommand
    {
        private readonly IPlayerService _playerService;

        public CreatePlayerCommand() : base()
        {
            _playerService = GameServices.Get<IPlayerService>();
        }

        protected override void ExecuteInternal()
        {
            _playerService.CreateNewPlayer();
            SucceedCommand();
        }
    }
}