using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;

namespace ZE.IsohECS {
	public class MonoToEntitySingletonTransferSystem
	{
        private World _world;
        private Dictionary<System.Type, Entity> _singletonBuffers = new();

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


        public Entity GetOrCreateSingletonBufferEntity<T>() where T:unmanaged, IBufferElementData
        {
            var type = typeof(T);
            Entity entity;
            if (!_singletonBuffers.TryGetValue(type, out entity))
            {
                entity = _world.EntityManager.CreateSingletonBuffer<T>();
                _singletonBuffers.Add(type, entity);
            }
            return entity;
        }
        public bool TryGetSingletonBuffer<T>(out DynamicBuffer<T> buffer) where T : unmanaged, IBufferElementData
        {
            var type = typeof(T);
            if (_singletonBuffers.TryGetValue(type, out var entity))
            {
                buffer = _world.EntityManager.GetBuffer<T>(entity);
                return !buffer.IsEmpty;
            }
            else
            {
                buffer = default;
                return false;
            }
        }
        public void AddValueToBuffer<T>(T value) where T : unmanaged, IBufferElementData
        {
            var entity = GetOrCreateSingletonBufferEntity<T>();
            _world.EntityManager.GetBuffer<T>(entity).Add(value);
        }
    }
}
