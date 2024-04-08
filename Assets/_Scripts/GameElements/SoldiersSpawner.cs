using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;

namespace ZE.IsohECS {
	public sealed class SoldiersSpawner : MonoBehaviour
	{
		[SerializeField] private GameObject _soldierPrefab;

        class Baker : Baker<SoldiersSpawner>
        {
            public override void Bake(SoldiersSpawner authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new SoldiersSpawnerComponent(GetEntity(authoring._soldierPrefab, TransformUsageFlags.Dynamic)));
            }
        }
    }
    public struct SoldiersSpawnerComponent : IComponentData
    {
        public readonly Entity Prefab;
        public SoldiersSpawnerComponent(Entity pref) { Prefab = pref; }
    }
}
