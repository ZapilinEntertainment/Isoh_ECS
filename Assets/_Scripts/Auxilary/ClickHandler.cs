using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;
using Unity.Physics;

//reference: https://github.com/WAYN-Games/DOTS-Training/blob/DOTS-110/Assets/Scripts/MonoBehaviours/PlayerManager.cs

namespace ZE.IsohECS {
	public class ClickHandler
	{
        private MonoToEntitySingletonTransferSystem _singletonSystem;
        private Entity _clickBufferEntity;

        public ClickHandler()
        {
            _singletonSystem = ServiceLocatorObject.Get<MonoToEntitySingletonTransferSystem>();
        }

        public void SelectClick(Vector2 screenPos) => UpdateValue(new(screenPos, false));
        public void SelectFrame(Rect rect) => UpdateValue(new(rect));
        public void ContextClick(Vector2 screenPos) => UpdateValue(new(screenPos, true));
       
        private void UpdateValue(PlayerClickData data) => _singletonSystem.UpdateSingletonValue(data);

        //not using buffer because at one moment there can be only one player input7

        /*
         *  private void Click(UnityEngine.Ray ray, bool context) {

            RaycastInput input = new RaycastInput()
            {
                Start = ray.origin,
                Filter = new CollisionFilter()
                {
                    BelongsTo = LayerSettings.GetDefinedLayerEntities(DefinedEntitiesLayer.PlayerClick),
                    CollidesWith = LayerSettings.GetEntitiesContactMask(context ? EntitiesContactMask.ContextAction : EntitiesContactMask.Selection)
                },
                End = ray.GetPoint(GameConstants.MAX_SELECTION_CAST_FAR)
            };

            PrepareBuffer(new ClickInfoBuffer(input, false));
        }
        private void PrepareBuffer<T>(T buffer) where T:unmanaged, IBufferElementData
        {
            if (_world.IsCreated && !_world.EntityManager.Exists(_clickBufferEntity))
            {
                _clickBufferEntity = _world.EntityManager.CreateEntity();
                _world.EntityManager.AddBuffer<T>(_clickBufferEntity);
            }

            _world.EntityManager.GetBuffer<T>(_clickBufferEntity).Add(buffer);
        }
        */
    }

    /*
    public readonly struct ClickInfoBuffer : IBufferElementData
    {
        public readonly RaycastInput Value;
        public readonly bool IsContextClick;

        public ClickInfoBuffer(RaycastInput input, bool isContext)
        {
            Value= input;
            IsContextClick= isContext;
        }
    }
    public readonly struct RectSelectionBuffer : IBufferElementData
    {
        public readonly Rect Value;

        public RectSelectionBuffer(Rect rect)
        {
            Value = rect;
        }
    }
    */
}
