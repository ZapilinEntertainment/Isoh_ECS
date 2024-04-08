using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZE.IsohECS
{
    [CreateAssetMenu(fileName = "CameraSettings", menuName = "ScriptableObjects/CameraSettingsObject", order = 1)]
    public sealed class CameraSettings : ScriptableObject
	{
		[SerializeField] private float _moveSpeed = 5f, _zoomSpeed = 2f;

		public float MoveSpeed => _moveSpeed;
		public float ZoomSpeed => _zoomSpeed;
	}
}
