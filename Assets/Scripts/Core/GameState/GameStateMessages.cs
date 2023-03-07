namespace mis.Core
{
    public readonly struct PauseGameMessage : IMessage { }
    public readonly struct ContinueGameMessage : IMessage { }
    public readonly struct QuitGameMessage : IMessage { }
    public readonly struct WinGameMessage : IMessage { }
    public readonly struct LoseGameMessage : IMessage { }
}