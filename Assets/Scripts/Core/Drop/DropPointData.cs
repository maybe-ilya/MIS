using System;
using UnityEngine;

namespace mis.Core
{
	[Serializable]
	public struct DropPointData {
		[SerializeField]
		public FloatRange XRange, YRange, ZRange;
	}
}