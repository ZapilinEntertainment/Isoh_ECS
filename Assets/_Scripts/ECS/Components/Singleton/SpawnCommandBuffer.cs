using Unity.Entities;
using Unity.Mathematics;

namespace ZE.IsohECS {
    public readonly struct SpawnCommandBuffer : IBufferElementData
    {
        public readonly bool IsSelected;
        public readonly float3 Position;
        public readonly quaternion Rotation;
        public readonly ApperanceInfoComponent AppearanceInfo;

        public SpawnCommandBuffer(bool isSelected, float3 position, quaternion rotation, ApperanceInfoComponent apperanceInfo)
        {
            IsSelected= isSelected;
            Position= position;
            Rotation= rotation;
            AppearanceInfo= apperanceInfo;
        }
    }
}
