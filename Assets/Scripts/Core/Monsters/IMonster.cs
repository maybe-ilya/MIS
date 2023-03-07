namespace mis.Core
{
    public interface IMonster : IGameEntityComponent
    {
        void Init(GameEntity target);
        void Deinit();
    }
}