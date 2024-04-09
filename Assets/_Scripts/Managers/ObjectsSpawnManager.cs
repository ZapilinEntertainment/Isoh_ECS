using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;
using UnityEngine.Pool;

namespace ZE.IsohECS {
	public sealed class ObjectsSpawnManager
	{
		private Dictionary<ModelID, VisualObject> _pools = new();

		public ObjectsSpawnManager()
		{
			var gameObjectsPack = ServiceLocatorObject.Get<GameElementsPack>();
			_pools.Add(ModelID.Soldier, gameObjectsPack.SoldierPrefab);
		}

		public VisualObject CreateObject(ApperanceInfoComponent appearanceInfo)
		{
			return Behaviour.Instantiate(_pools[appearanceInfo.ModelID]); // todo: change to pooling
		}
	}
}
