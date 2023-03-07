namespace mis.Core
{
    public interface IUIService : IService
    {
        IHUD OpenHUD(GameId hudId);
        void CloseHUD(GameId hudID);
        IWindow OpenWindow(GameId windowId);
        void CloseWindow(GameId windowId);
    }
}