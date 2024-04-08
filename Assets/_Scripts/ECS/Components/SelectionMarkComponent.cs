using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace ZE.IsohECS {
	public struct SelectionMarkComponent : IComponentData
	{
		public int FollowingUnitID;
		public float3 WorldPosition;
	}
}
