using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;
using Unity.Transforms;

namespace ZE.IsohECS {
	public partial struct TestSystem : ISystem
	{
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;

            // Loop over every entity having a LocalTransform component and RotationSpeed component.
            // In each iteration, transform is assigned a read-write reference to the LocalTransform,
            // and speed is assigned a read-only reference to the RotationSpeed component.
            foreach (var (transform, speed) in
                     SystemAPI.Query<RefRW<LocalTransform>, RefRO<TestComponentData>>())
            {
                // ValueRW and ValueRO both return a ref to the actual component value.
                // The difference is that ValueRW does a safety check for read-write access while
                // ValueRO does a safety check for read-only access.
                transform.ValueRW = transform.ValueRO.RotateY(
                    speed.ValueRO.RotationSpeed * deltaTime);
            }
        }
    }
}
