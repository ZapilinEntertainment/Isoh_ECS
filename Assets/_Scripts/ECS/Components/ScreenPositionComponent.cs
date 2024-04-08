using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;
using Unity.Mathematics;

namespace ZE.IsohECS {
	public struct ScreenPositionComponent : IComponentData
	{
		public float2 Value;
	}
}
