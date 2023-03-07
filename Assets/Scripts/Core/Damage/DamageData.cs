using System;
using UnityEngine;

namespace mis.Core
{
	[Serializable]
	public struct DamageData {
		[SerializeField]
		public UintRange DamageRange;
		[SerializeField]
		public bool IsDistanceDependentDamage;
		[SerializeField]
		public float MaxDistance;
		[SerializeField]
		public AnimationCurve DamageDistanceCurve;
	}
}