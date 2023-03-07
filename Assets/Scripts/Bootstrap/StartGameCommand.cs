using mis.Core;

public class StartGameCommand : AbstractCommand
{
    private readonly IMessageService _messageService;

    public StartGameCommand()
    {
        _messageService = GameServices.Get<IMessageService>();
    }

    protected override void ExecuteInternal()
    {
        _messageService.Send<StartGameMessage>();
        SucceedCommand();
    }
}