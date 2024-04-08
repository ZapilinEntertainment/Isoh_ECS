using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZE.IsohECS
{
    [CreateAssetMenu(fileName = "GameElementsPack", menuName = "ScriptableObjects/GameElementsPack", order = 1)]
    public sealed class GameElementsPack : ScriptableObject
	{
		[field:SerializeField] public GameObject SelectionMark { get; private set; }
	}
}
