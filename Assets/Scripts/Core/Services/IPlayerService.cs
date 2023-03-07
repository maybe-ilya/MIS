namespace mis.Core
{
    public interface IPlayerService : IService
    {
        IPlayer CreateNewPlayer();
        IPlayer GetPlayer(int index);
        IPlayer GetFirstPlayer();
        IPlayerController GetPlayerController(int index);
        IPlayerController GetFirstPlayerController();
    }
}
