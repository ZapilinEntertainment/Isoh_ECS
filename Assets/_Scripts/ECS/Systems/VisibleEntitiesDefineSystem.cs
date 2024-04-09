using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;

namespace ZE.IsohECS {

    // checks if unit visible or not - by using spherical culling
    // freezed

    /*

    [UpdateInGroup(typeof(PresentationSystemGroup))]
    [UpdateAfter(typeof(UnitSelectionSystem))]
    public partial struct VisibleEntitiesDefineSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            using (var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob))
            {
                var notDrawnObjects = SystemAPI.QueryBuilder().WithAll<VisualizableObjectTag>().WithNone<ApperanceInfoComponent>().;
                foreach (var entity in notDrawnObjects.)
                {

                }

            }

            foreach (var transform in  SystemAPI.Query<RefRO<LocalTransform>, RefRO<QueryI>>().WithAll<VisualizableObjectTag>().WithNone<ApperanceInfoComponent>())
            {
               
            }


            Entities.WithAll<ShouldSpawnTag>().ForEach((Entity e, int entityInQueryIndex, ref EntitySpawnData spawnData,
                in Translation translation) =>
            {
                spawnData.Timer -= deltaTime;
                if (spawnData.Timer <= 0)
                {
                    spawnData.Timer = spawnData.SpawnDelay;
                    var newEntity = ecb.Instantiate(entityInQueryIndex, spawnData.EntityToSpawn);
                    ecb.AddComponent<CapsuleTag>(entityInQueryIndex, newEntity);
                    ecb.SetComponent(entityInQueryIndex, newEntity, translation);
                }
            }).ScheduleParallel();
            _endSimulationEntityCommandBufferSystem.AddJobHandleForProducer(this.Dependency);
        }

        public partial struct VisibilityDefineJob : IJobEntity
        {
            public EntityCommandBuffer.ParallelWriter Writer;
            public CameraInfoContainerSingleton CameraInfo;

            public VisibilityDefineJob(EntityCommandBuffer.ParallelWriter writer)
            {
                Writer = writer;
            }

            [BurstCompile]
            public void Execute(in LocalTransform transform, in ApperanceInfoComponent appearanceInfo)
            {
                bool isVisible = false;
                isVisible = true;
                var screenPos = CameraInfo.WorldToNormalizedScreenCoordinates(transform.Position);
                Writer.SetComponentEnabled<VisibilityStatusComponent>(screenPos.x);

                [BurstCompile]
                bool IsPointVisible(float2 point) => point.x > 0 && point.y > 0 && point.x < 1f && point.y < 1f;
            }

            
        }
    }

    */
}
