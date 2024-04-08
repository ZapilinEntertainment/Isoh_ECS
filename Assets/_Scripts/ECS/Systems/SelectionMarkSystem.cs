using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;

namespace ZE.IsohECS {

    [UpdateInGroup(typeof(PresentationSystemGroup))]
    public partial struct SelectionMarkSystem : ISystem
    {
        private EntityArchetype _markerArchetype;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            // This call makes the system not update unless at least one entity in the world exists that has the Spawner component.
            //state.RequireForUpdate<Spawner>();
            //_markerArchetype = state.EntityManager.CreateArchetype(typeof(SelectionMarkComponent), typeof(LocalToWorld), typeof(renderMe))
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var markedUnits = SystemAPI.QueryBuilder().WithAll<SelectedUnitTag>().Build();
            int markedUnitsCount = markedUnits.CalculateEntityCount();
            var markers = SystemAPI.QueryBuilder().WithAll<SelectionMarkComponent>().Build();
            int markersCount = markers.CalculateEntityCount();
            if (markedUnitsCount != markersCount)
            {
              //  int delta = markedUnitsCount - markersCount;
               // state.EntityManager.CreateEntity(new EntityArchetype(), delta);
            }
            

            //unmarkedUnits
          

            // drawing

            foreach (var (transform, marker) in
                     SystemAPI.Query<RefRW<LocalTransform>, RefRO<SelectionMarkComponent>>())
            {
                transform.ValueRW.Position = marker.ValueRO.WorldPosition;
            }
        }
    }
}
