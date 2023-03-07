namespace mis.Core
{
    public readonly struct SwitchLevelMessage : IMessage
    {
        public readonly GameId PreviousLevelId, NextLevelId;

        public SwitchLevelMessage(GameId previousLevelId, GameId nextLevelId)
        {
            PreviousLevelId = previousLevelId;
            NextLevelId = nextLevelId;
        }
    }

    public readonly struct LevelLoadStartedMessage : IMessage
    {
        public readonly GameId LevelId;

        public LevelLoadStartedMessage(GameId levelId)
        {
            LevelId = levelId;
        }
    }

    public readonly struct LevelLoadFinishedMessage : IMessage
    {
        public readonly GameId LevelId;

        public LevelLoadFinishedMessage(GameId levelId)
        {
            LevelId = levelId;
        }
    }

    public readonly struct LevelStartedMessage : IMessage
    {
        public readonly GameId LevelId;

        public LevelStartedMessage(GameId levelId)
        {
            LevelId = levelId;
        }
    }
}