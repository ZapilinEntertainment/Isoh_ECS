using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;
using Unity.Mathematics;

namespace ZE.IsohECS {

	public enum ModelID : int { Undefined, Soldier}

	public struct ApperanceInfoComponent : IComponentData
	{
		public ModelID ModelID; // todo: change to struct info container
	}
}
