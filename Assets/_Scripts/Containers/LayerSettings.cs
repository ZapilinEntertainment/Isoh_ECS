using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZE.IsohECS
{
    public enum CustomLayermask : byte { Default, UnitsSelection, ContextOrdersMask }
    public enum DefinedLayer : byte { Default, Unit, Terrain }


    public static class LayerSettings
	{
        public const string DEFAULT_LAYERNAME = "Default", UNIT_LAYERNAME = "Unit", TERRAIN_LAYERNAME = "Terrain";
        

        private static Dictionary<CustomLayermask, int> _customLayermasks = new Dictionary<CustomLayermask, int>();
        private static Dictionary<DefinedLayer, int> _definedLayers = new Dictionary<DefinedLayer, int>();

        
        
        public static int GetDefinedLayer(DefinedLayer definedLayer)
        {
            int layer = 0;
            if (!_definedLayers.TryGetValue(definedLayer, out layer))
            {
                string layerName;
                switch (definedLayer)
                {
                    case DefinedLayer.Terrain: layerName = TERRAIN_LAYERNAME;break;
                    case DefinedLayer.Unit: layerName = UNIT_LAYERNAME;break;
                    default: layerName = DEFAULT_LAYERNAME; break;
                }
                layer = LayerMask.NameToLayer(layerName);
                _definedLayers.Add(definedLayer, layer);
            }
            return layer;
        }   
        public static int GetCustomLayermask(CustomLayermask customLayer)
        {
            if (!_customLayermasks.TryGetValue(customLayer, out int value))
            {
                switch (customLayer)
                {
                    case CustomLayermask.UnitsSelection: value = LayerMask.GetMask(UNIT_LAYERNAME); break;
                    case CustomLayermask.ContextOrdersMask: value = LayerMask.GetMask(UNIT_LAYERNAME, TERRAIN_LAYERNAME); break;
                    default: value = LayerMask.GetMask(DEFAULT_LAYERNAME); break;
                }
                _customLayermasks.Add(customLayer, value);
            }
            return value;
        }


        public static uint GetDefinedLayer_Entities(DefinedLayer definedLayer) => (uint)(1 << GetDefinedLayer(definedLayer));
        public static uint GetCustomLayermask_Entities(CustomLayermask customLayer)
        {
            switch (customLayer)
            {
                case CustomLayermask.UnitsSelection: return GetDefinedLayer_Entities(DefinedLayer.Unit); 
                case CustomLayermask.ContextOrdersMask: return GetDefinedLayer_Entities(DefinedLayer.Unit) | GetDefinedLayer_Entities(DefinedLayer.Terrain); 
                default: return uint.MaxValue;;
            }
        }
    }
}
