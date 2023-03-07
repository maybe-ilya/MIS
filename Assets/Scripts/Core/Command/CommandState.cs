namespace mis.Core
{
    public enum CommandState : uint
    {
        NotStarted = 0,
        InProgress = 1 << 0,
        Success = 1 << 1,
        Failure = 1 << 2,
    }
}
