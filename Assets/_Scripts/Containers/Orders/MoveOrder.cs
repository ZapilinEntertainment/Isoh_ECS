using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZE.IsohECS
{
	public struct MoveOrder : IUnitOrder
	{
		public Vector3 Point { get; private set; }
		public OrderType OrderType => OrderType.Move;
		public MoveOrder (Vector3 pos)
		{
            Point = pos;
		}
	}
}
