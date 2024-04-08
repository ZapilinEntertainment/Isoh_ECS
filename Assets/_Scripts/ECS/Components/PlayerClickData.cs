using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;
using Unity.Mathematics;

namespace ZE.IsohECS {
	public struct PlayerClickData : IComponentData
	{
		public bool ClickProcessed, UseFrameSelection, IsContextClick;
		public float2 ClickScreenPos;
		public float2 SelectionFrameSize;

		public PlayerClickData(float2 screenPos, bool contextClick)
		{
			ClickProcessed = false;
			ClickScreenPos = screenPos;
			UseFrameSelection = false;
			IsContextClick = contextClick;
			SelectionFrameSize = float2.zero;
		}
		public PlayerClickData(Rect rect)
		{
			ClickProcessed = false;
			UseFrameSelection = true;
			IsContextClick = false;
			ClickScreenPos = rect.position;
			SelectionFrameSize = rect.size;
		}
	}
}
