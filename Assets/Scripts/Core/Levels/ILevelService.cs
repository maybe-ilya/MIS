namespace mis.Core
{
    public interface ILevelService : IService
    {
        void LoadLevel(GameId levelId);
        void ReloadLastLevel();
    }
}