namespace mis.Core
{
    public readonly struct PlayerSpawnedMessage : IMessage
    {
        public readonly IPlayer SpawnedPlayer;

        public PlayerSpawnedMessage(IPlayer player)
        {
            SpawnedPlayer = player;
        }
    }

    public readonly struct PlayerControlledPawnMessage : IMessage
    {
        public readonly int PlayerIndex;
        public readonly IPlayerPawn ControlledPawn;

        public PlayerControlledPawnMessage(int index, IPlayerPawn pawn)
        {
            PlayerIndex = index;
            ControlledPawn = pawn;
        }
    }

    public readonly struct PlayerReleasedPawnMessage : IMessage
    {
        public readonly int PlayerIndex;
        public readonly IPlayerPawn ReleasedPawn;

        public PlayerReleasedPawnMessage(int index, IPlayerPawn pawn)
        {
            PlayerIndex = index;
            ReleasedPawn = pawn;
        }
    }
}