using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;
using Unity.Physics;

namespace ZE.IsohECS {
	public readonly struct LayerSettingsData : IComponentData
	{
        public readonly CollisionFilter PlayerSelectionClickFilter, PlayerContextClickFilter;
        public LayerSettingsData(int group = 0)
		{
            uint playerClickLayer = LayerSettings.GetDefinedLayer_Entities(DefinedLayer.Default);

            PlayerSelectionClickFilter = new CollisionFilter
            {
                BelongsTo = playerClickLayer,
                CollidesWith = LayerSettings.GetCustomLayermask_Entities(CustomLayermask.UnitsSelection)
            };
            PlayerContextClickFilter = new CollisionFilter
            {
                BelongsTo = playerClickLayer,
                CollidesWith = LayerSettings.GetCustomLayermask_Entities(CustomLayermask.ContextOrdersMask)
            };
        }
	}
}
