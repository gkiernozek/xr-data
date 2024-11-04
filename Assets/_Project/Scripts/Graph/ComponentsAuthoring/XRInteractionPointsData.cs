using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace XRData
{
    public struct XRInteractionPointsData : IComponentData
    {
        public NativeList<float3> XRInteractionPositions;
    }
}