using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace XRData
{
    public struct XRInteractionPointsDataComponent : IComponentData
    {
        public NativeList<float3> XRInteractionPositions;
    }
}