using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;
using Unity.Mathematics;

namespace ZE.IsohECS {
	public readonly struct UnitVisualInfo
	{
		public readonly float3 Position;
		public readonly quaternion Rotation;
	}
}
