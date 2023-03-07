namespace mis.Core
{
	public interface IMusicClipSource : IGameEntityComponent {
		bool IsPlaying { get; }
		bool IsLooped { get; }
		public void Play(IMusicClipConfig config);
		void Stop(bool instantly = false);
	}
}