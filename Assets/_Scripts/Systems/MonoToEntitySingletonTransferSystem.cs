using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;

namespace ZE.IsohECS {
	public class MonoToEntitySingletonTransferSystem
	{
        private World _world;

        public MonoToEntitySingletonTransferSystem()
        {
            _world = World.DefaultGameObjectInjectionWorld;
        }
        public void UpdateSingletonValue<T>(T val) where T: unmanaged,IComponentData
		{
            var query = _world.EntityManager.CreateEntityQuery(typeof(T));
            if (query.TryGetSingletonRW<T>(out var value))
            {
                value.ValueRW = val;
            }
            else
            {
                _world.EntityManager.CreateSingleton<T>(val);
            }
        }
	}
}
