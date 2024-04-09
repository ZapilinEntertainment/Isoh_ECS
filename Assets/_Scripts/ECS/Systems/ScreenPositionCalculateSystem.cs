using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Burst;
using Unity.Mathematics;

namespace ZE.IsohECS {
    [BurstCompile]
	public partial struct ScreenPositionCalculateSystem : ISystem
	{
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var cameraInfo = SystemAPI.GetSingleton<CameraInfoContainerSingleton>();
            var entityQuery = SystemAPI.QueryBuilder().WithAll<LocalTransform>().WithAllRW<ScreenPositionComponent>().Build();
            new ScreenPositionCalculateJob() { CameraInfo = cameraInfo}.ScheduleParallel(entityQuery);
        }

        public partial struct ScreenPositionCalculateJob : IJobEntity
        {
            public CameraInfoContainerSingleton CameraInfo;

            [BurstCompile]
            public void Execute(in LocalTransform transform, ref ScreenPositionComponent screenPos)
            {
                screenPos.Value = CameraInfo.WorldToNormalizedScreenCoordinates(transform.Position);
            }
        }      
    }
}
