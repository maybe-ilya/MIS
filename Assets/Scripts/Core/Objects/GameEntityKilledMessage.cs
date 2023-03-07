namespace mis.Core
{
    public class GameEntityKilledMessage : IMessage
    {
        public GameEntity KilledEntity { get; private set; }

        public GameEntityKilledMessage(GameEntity killedEntity)
        {
            this.KilledEntity = killedEntity;
        }
    }
}