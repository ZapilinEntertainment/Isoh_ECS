using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;

namespace ZE.IsohECS {
	public sealed class TestComponent : MonoBehaviour
	{
		[SerializeField] private float _rotationSpeed = 90f;

        class TestComponentBaker : Baker<TestComponent>
        {
            public override void Bake(TestComponent authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Renderable|TransformUsageFlags.Dynamic );
                AddComponent(entity, new TestComponentData
                {
                    // By default, each authoring GameObject turns into an Entity.
                    // Given a GameObject (or authoring component), GetEntity looks up the resulting Entity.
                    RotationSpeed = authoring._rotationSpeed
                }) ;
            }
        }
    }
    public struct TestComponentData : IComponentData
    {
        public float RotationSpeed;
    }
}
