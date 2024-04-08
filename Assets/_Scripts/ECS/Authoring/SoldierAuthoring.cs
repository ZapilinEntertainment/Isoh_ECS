using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;

namespace ZE.IsohECS {
	public sealed class SoldierAuthoring : MonoBehaviour
	{
        class Baker : Baker<SoldierAuthoring>
        {
            public override void Bake(SoldierAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<SoldierComponent>(entity);
                AddComponent<ScreenPositionComponent>(entity);
            }
        }
    }
}
