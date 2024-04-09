using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using Unity.Jobs;
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Physics;
using ZE.ServiceLocator;

//reference: https://github.com/WAYN-Games/DOTS-Training/blob/DOTS-110/Assets/Scripts/Systems/InputSystem.cs

namespace ZE.IsohECS {
    [BurstCompile]
    public partial struct UnitSelectionSystem : ISystem
    {

       [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if (
                SystemAPI.TryGetSingleton<CameraInfoContainerSingleton>(out var cameraInfo) &&
                SystemAPI.TryGetSingleton<PlayerClickData>(out var playerClickData) &&
                !playerClickData.ClickProcessed
                )
            {                

                var physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
                var collisionFilters = SystemAPI.GetSingleton<LayerSettingsData>();
                if (!playerClickData.UseFrameSelection)
                {
                    var ray = cameraInfo.ScreenPointToRay(playerClickData.ClickScreenPos);
                    var raycastInput = new RaycastInput()
                    {
                        Start = ray.start,
                        Filter = playerClickData.IsContextClick ? collisionFilters.PlayerContextClickFilter : collisionFilters.PlayerSelectionClickFilter,
                        End = ray.start + GameConstants.MAX_SELECTION_CAST_FAR * ray.direction
                    };

                    if (physicsWorld.CastRay(raycastInput, out var hit))
                    {
                        //check for selectable

                        var entity = physicsWorld.CollisionWorld.Bodies[hit.RigidBodyIndex].Entity;
                        state.EntityManager.AddComponent<SelectionMarkComponent>(entity);
                    }
                }
                else
                {
                    // make a rect selection check
                }

                SystemAPI.SetSingleton(new PlayerClickData() { ClickProcessed = true }); ;
            }            
        }

        public async void TrySelect(Rect selectionRect)
        {
            /*
            var cameraInfo = CameraController.GetCameraPositionInfo();
             DropSelection();
            var units = UnitSystem.ReadUnits();
            int unitsCount = units.Count;
            if (unitsCount == 0) return;

            NativeArray<float3> positions = new(unitsCount, Allocator.TempJob);
            var ids = new int[unitsCount];
            int i = 0;
            foreach (var unit in units)
            {
                positions[i] = unit.VisiblePosition;
                ids[i] = unit.ID;
                i++;
            }
            NativeArray<bool> selectionMask = new NativeArray<bool>(unitsCount, Allocator.TempJob);
            var job = new UnitSelectionJob()
            {
                _unitPositions = positions,
                _selectedUnits = selectionMask,
                CameraPosition = cameraInfo.Position,
                CameraForward = cameraInfo.Forward,
                CameraAngleDot = cameraInfo.ViewAngleDot
            };
            var jobHandle = job.Schedule(unitsCount, 64);
            await Task.Run(() =>
            {
                while (!jobHandle.IsCompleted) Task.Yield();
            });
            jobHandle.Complete();
            
            for(i = 0; i < unitsCount; i++)
            {
                if (selectionMask[i])
                {
                    Vector2 point = _cameraController.GetScreenPosition(positions[i]);
                    if (selectionRect.Contains(point) && unitSystem.TryGetUnitById(ids[i], out var unit))
                    {
                        _selectedUnits.Add(unit);
                    }
                }
            }
            positions.Dispose();
            selectionMask.Dispose();
            OnUnitsSelected();
            */
        }

        [BurstCompile]
        private struct UnitSelectionJob : IJobParallelFor
        {
            [ReadOnly] public float CameraAngleDot;
            [ReadOnly] public float3 CameraPosition, CameraForward;
            [ReadOnly] public NativeArray<float3> _unitPositions;
            [WriteOnly] public NativeArray<bool> _selectedUnits;
            private const float MAX_FAR = GameConstants.MAX_SELECTION_CAST_FAR;

            public void Execute(int i)
            {                
                float3 dir = _unitPositions[i] - CameraPosition;
                bool unitSelected = false;
                if (math.dot(dir, CameraForward) > CameraAngleDot)
                {
                    float far = math.length(dir);
                    if (far > 0f & far < MAX_FAR)
                    {
                        unitSelected = true;
                    }
                }
                _selectedUnits[i] = unitSelected;
            }
        }

        
    }
}
