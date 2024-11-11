using Unity.Entities;
using UnityEngine;

namespace XRData
{
    public struct GraphPivotTransformData : IComponentData
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }
}