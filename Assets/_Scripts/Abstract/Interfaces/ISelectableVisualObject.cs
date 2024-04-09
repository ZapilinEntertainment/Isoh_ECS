using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;

namespace ZE.IsohECS {
	public interface ISelectableVisualObject
	{
		public Transform SelectionAnchor { get; }
	}
}
