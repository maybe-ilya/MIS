namespace mis.Core
{
    public abstract class AbstractSoundCuePlayer : AbstractGameEntityComponent
    {
        public abstract void ApplyCue(AbstractSoundCue cue);
        public abstract void Play();
        public abstract void Play(AbstractSoundCue cue);
        public abstract void Stop();
        public abstract T GetOrCreateState<T>() where T : class, ISoundCueState, new();
    }
}