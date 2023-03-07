using UnityEngine;

namespace mis.Core
{
    public abstract class AbstractSoundCue : ScriptableObject
    {
        public abstract void PrepareSource(AudioSource audioSource, AbstractSoundCuePlayer player);
    }
}