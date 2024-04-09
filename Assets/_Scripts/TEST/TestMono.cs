using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;

namespace ZE.IsohECS {
	public sealed class TestMono : MonoBehaviour
	{
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                ServiceLocatorObject.Get<MonoToEntitySingletonTransferSystem>().AddValueToBuffer<SpawnCommandBuffer>(
                    new SpawnCommandBuffer(false, Vector3.zero, Quaternion.identity, new ApperanceInfoComponent() { ModelID = ModelID.Soldier})
                    );
            }
        }
    }
}
