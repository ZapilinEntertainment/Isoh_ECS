using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZE.IsohECS
{
    public static class GameConstants
    {        
        public const int MAX_SELECTION_UNITS = 1000, MAX_ORDERS_COUNT = 5;
        public const float MAX_SELECTION_CAST_FAR = 250f;
        public const string ORDERABLE_TERRAIN_TAG = LayerSettings.TERRAIN_LAYERNAME;
    
    }
}
