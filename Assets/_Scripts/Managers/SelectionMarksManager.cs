using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;
using UnityEngine.Pool;

namespace ZE.IsohECS {
	public sealed class SelectionMarksManager
	{
		private GameObject _markerObjectPrefab;

		public SelectionMarksManager()
		{
			_markerObjectPrefab = ServiceLocatorObject.Get<GameElementsPack>().SelectionMark;
		}

		public void AddVisualSelection(ISelectableVisualObject gameObject)
		{
			//todo: change to pooling
			var marker = Behaviour.Instantiate(_markerObjectPrefab);
			marker.transform.SetParent(gameObject.SelectionAnchor, false);
		}
	}
}
