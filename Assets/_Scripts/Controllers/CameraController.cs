using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ZE.ServiceLocator;
using Unity.Entities;
using Unity.Mathematics;

namespace ZE.IsohECS {

	public struct CameraPositionInfo
	{
		public Vector3 Position, Forward;
		public float ViewAngleDot;
	}
	public sealed class CameraController : SessionObject
	{
		[SerializeField] private Transform _viewObject;
		[SerializeField] private Camera _camera;
		[SerializeField] private CameraSettings _cameraSettings;
		private Transform _cameraTransform;
		private MonoToEntitySingletonTransferSystem _singletonSystem;
		

		public Vector2 GetScreenPosition(Vector3 worldPos) => _camera.WorldToScreenPoint(worldPos);
		public Ray FormCameraRay(Vector2 screenPos) => _camera.ScreenPointToRay(screenPos);
		public CameraPositionInfo GetCameraPositionInfo() => new CameraPositionInfo()
		{
			Position = _cameraTransform.position,
			Forward = _cameraTransform.forward,
			ViewAngleDot = Mathf.Cos(_camera.fieldOfView)
		};

        protected override void OnAwake()
        {
			_cameraTransform = _camera.transform;
        }

        protected override void OnStart()
        {
			_singletonSystem = ServiceLocatorObject.Get<MonoToEntitySingletonTransferSystem>();
			ServiceLocatorObject.GetWhenLinkReady<InputController>(OnInputControllerReady);
        }

        private void OnInputControllerReady(InputController controller)
		{
            controller.OnCameraMoveEvent += OnCameraMove;
            controller.OnCameraZoomEvent += OnCameraZoom;
        }
		private void OnCameraMove(Vector2 dir) {
			Vector3 right, fwd;
			if (dir.x != 0f)
			{
				right = _cameraTransform.right;
				right.y = 0f;
				right.Normalize();
			}
			else right = Vector3.zero;
			if (dir.y != 0f)
			{
				fwd = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up);
				fwd.Normalize();
			}
			else fwd = Vector3.zero;

			float speed = _cameraSettings.MoveSpeed * Time.deltaTime;
			_viewObject.transform.position += speed * dir.x * right  + speed * dir.y * fwd;
		}
		private void OnCameraZoom(float zoom) {
			_viewObject.Translate(zoom * _cameraSettings.ZoomSpeed * Time.deltaTime * _cameraTransform.forward);
		}

        private void Update()
        {
			_singletonSystem.UpdateSingletonValue(
				new CameraInfoContainerSingleton(
					Screen.width,
					Screen.height,
					_cameraTransform.position,
					_camera.projectionMatrix,
					_camera.cameraToWorldMatrix
					)
				);         	
        }
    }
}
