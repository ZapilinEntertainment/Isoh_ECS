using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZE.IsohECS {
	public interface IUnitOrder 
	{
		public Vector3 Point { get; }
		public OrderType OrderType { get; }
	}
}
