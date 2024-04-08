using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;

namespace ZE.IsohECS {
	public sealed class ScreenDrawTestModule : MonoBehaviour
	{
        private World _world;
        private void Awake()
        {
            _world = World.DefaultGameObjectInjectionWorld;
        }

#if UNITY_EDITOR
        private void OnGUI()
        {
            if (Application.isPlaying && enabled)
            {
                var positions = _world.EntityManager.CreateEntityQuery(ComponentType.ReadOnly<ScreenPositionComponent>()).ToComponentDataArray<ScreenPositionComponent>(Allocator.Temp);
                if (positions.Length > 0)
                {
                    foreach (var item in positions)
                    {
                        GUI.Label(new Rect(item.Value.x * Screen.width, (1f - item.Value.y) * Screen.height, 50f, 50f), "here");
                    }
                }
                GUILayout.Label(positions.Length.ToString());
            }
        }
#endif
    }
}
