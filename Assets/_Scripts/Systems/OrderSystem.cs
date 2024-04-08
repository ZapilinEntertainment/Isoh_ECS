using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;

namespace ZE.IsohECS {
    public sealed class OrderSystem 
    {
        private UnitSelectionSystem _selectionSystem;
        
        public OrderSystem()
        {
            ServiceLocatorObject.GetWhenLinkReady((UnitSelectionSystem sys) => _selectionSystem = sys);
        }

        public void ContextOrder(RaycastHit hit)
        {
            var collider = hit.collider;
            if (collider.CompareTag(GameConstants.ORDERABLE_TERRAIN_TAG))
            {
                MoveOrder(hit.point);
            }
            else
            {

            }
        }
        public void MoveOrder(Vector3 point, bool add = false)
        {
            /*
            var units = _selectionSystem.GetSelectedUnits();
            if (units.Count != 0)
            {
                var order = new MoveOrder(point);
                if (!add)
                {
                    foreach (var unit in units)
                    {
                        unit.ControlModule.SetOrder(order);
                    }
                }
                else
                {
                    foreach (var unit in units)
                    {
                        unit.ControlModule.AddOrder(order);
                    }
                }
            }
            */
        }
    }
}
