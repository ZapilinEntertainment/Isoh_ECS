using System.Collections;
using System.Collections.Generic;
using ZE.ServiceLocator;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Burst;

//source: https://forum.unity.com/threads/getting-camera-worldtoscreenpoint-player-translation-here-data-into-a-job.686539/

namespace ZE.IsohECS {
    [BurstCompile]
	public readonly struct CameraInfoContainerSingleton : IComponentData
	{
        public readonly float ResolutionWidth, ResolutionHeight;
        public readonly float3 CameraPosition;
        public readonly float4x4 CameraProjectionMatrix, CameraToWorldMatrix;

        public CameraInfoContainerSingleton (float resWidth, float resHeight, float3 cameraPosition, float4x4 cameraProjectionMatrix, float4x4 cameraToWorldMatrix)
        {
            ResolutionWidth = resWidth;
            ResolutionHeight = resHeight;

            CameraPosition = cameraPosition;
            CameraProjectionMatrix = cameraProjectionMatrix;

            CameraToWorldMatrix = cameraToWorldMatrix;
        }
        [BurstCompile]
        public float2 ConvertWorldToNormalizedScreenCoordinates(float3 point)
        {
            /*
            * 1 convert P_world to P_camera
            */
            float4 pointInCameraCoodinates = ConvertWorldToCameraCoordinates(point, CameraPosition);
            /*
            * 2 convert P_camera to P_clipped
            */
            float4 pointInClipCoordinates = math.mul(CameraProjectionMatrix, pointInCameraCoodinates);
            /*
            * 3 convert P_clipped to P_ndc
            * Normalized Device Coordinates
            */
            float4 pointInNdc = pointInClipCoordinates / pointInClipCoordinates.w;
            /*
            * 4 convert P_ndc to P_screen
            */
            float2 pointInScreenCoordinates;
            pointInScreenCoordinates.x = 0.5f * (pointInNdc.x + 1);
            pointInScreenCoordinates.y = 0.5f * (pointInNdc.y + 1);
            return pointInScreenCoordinates;
        }

        [BurstCompile]
        private float4 ConvertWorldToCameraCoordinates(float3 point, float3 cameraPos)
        {
            // translate the point by the negative camera-offset
            //and convert to Vector4
            float4 translatedPoint = new float4(point - cameraPos, 1f);           
            float4 transformedPoint = math.mul(math.inverse(CameraToWorldMatrix), translatedPoint);
            return transformedPoint;
        }

        [BurstCompile]
        public (float3 start, float3 direction) ScreenPointToRay(float2 screenPosition)
        {
            screenPosition = new float2(screenPosition.x, ResolutionHeight - screenPosition.y);
            float4 clipSpacePos = new float4(((screenPosition.x * 2.0f) / ResolutionWidth) - 1.0f, (1.0f - (2.0f * screenPosition.y) / ResolutionHeight), 0.0f, 1.0f);
            var inversedProjection = math.inverse(CameraProjectionMatrix);
            float4 viewSpacePos = math.mul(inversedProjection, clipSpacePos);
            viewSpacePos /= viewSpacePos.w;

            float3 worldSpacePos = math.mul(CameraToWorldMatrix, viewSpacePos).xyz;
            float3 worldDirection = math.normalize(worldSpacePos - CameraPosition);

            return (worldSpacePos, worldDirection);
        }
    }
}
