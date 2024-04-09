using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;

namespace ZE.IsohECS {
	public class SelectableVisualObject : VisualObject, ISelectableVisualObject
	{
		[SerializeField] private Transform _selectionAnchor;
		public Transform SelectionAnchor => _selectionAnchor;
	}
}
