using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZE.IsohECS
{
    [CreateAssetMenu(fileName = "UnitStats", menuName = "ScriptableObjects/UnitStats", order = 2)]
    public class UnitStats : ScriptableObject
	{
		[SerializeField] private float _speed = 1f, _rotationSpeed = 360f;

		public float Speed => _speed;
		public float RotationSpeed => _rotationSpeed;
	}
}
