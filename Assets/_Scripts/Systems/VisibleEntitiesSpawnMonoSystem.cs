using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;


namespace ZE.IsohECS {
	public sealed class VisibleEntitiesSpawnMonoSystem : MonoBehaviour
	{
		private bool _isReady = false;
		private ComplexResolver<ObjectsSpawnManager, SelectionMarksManager, MonoToEntitySingletonTransferSystem> _resolver;

		private ObjectsSpawnManager SpawnManager => _resolver.Item1;
		private SelectionMarksManager SelectionMarksManager => _resolver.Item2;
		private MonoToEntitySingletonTransferSystem SingletonTransferSystem => _resolver.Item3;

		private void Start ()
		{
			_resolver = new(() => _isReady = true);
			_resolver.CheckDependencies();
		}
        private void Update()
        {
			if (!_isReady) return;
            if (SingletonTransferSystem.TryGetSingletonBuffer<SpawnCommandBuffer>(out var buffer))
			{
                foreach (var spawnInfo in buffer)
                {
					var model = SpawnManager.CreateObject(spawnInfo.AppearanceInfo);
					model.transform.SetPositionAndRotation(spawnInfo.Position, spawnInfo.Rotation);
					if (spawnInfo.IsSelected && model is ISelectableVisualObject visualObject)
					{
						SelectionMarksManager.AddVisualSelection(visualObject);
					}
                }
				buffer.Clear();
            }
        }

		//add similar system to remove entities, to add and remove selection marks
    }	
}
