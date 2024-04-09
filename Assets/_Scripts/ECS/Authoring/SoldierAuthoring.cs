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
                var appearance = new ApperanceInfoComponent() { ModelID = ModelID.Soldier };
                AddComponent<ApperanceInfoComponent>(entity, appearance);

                if (Application.isPlaying)
                {
                    ServiceLocatorObject.Get<MonoToEntitySingletonTransferSystem>().AddValueToBuffer(new SpawnCommandBuffer(false, authoring.transform.position, authoring.transform.rotation, appearance));
                }
            }
        }
    }
}
