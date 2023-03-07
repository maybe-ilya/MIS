namespace mis.Core
{
	public interface IGameStateService : IService {
		bool IsGamePaused { get; }
		void PauseGame();
		void ContinueGame();
		void QuitGame();
	}
}