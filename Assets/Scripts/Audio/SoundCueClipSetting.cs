using mis.Core;
using System;
using UnityEngine;

namespace mis.Audio
{
	[Serializable]
	public sealed class SoundCueClipSetting {
		[SerializeField]
		[CheckObject]
		public AudioClip Clip;

		[SerializeField]
		public FloatRange VolumeRange;

		[SerializeField]
		public FloatRange PitchRange;
	}
}