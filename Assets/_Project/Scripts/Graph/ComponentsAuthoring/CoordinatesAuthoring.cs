using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct Coordinates : IComponentData
{
    public int2 xz;
}

class CoordinatesAuthoring : MonoBehaviour
{
    class CoordinatesAuthoringBaker : Baker<CoordinatesAuthoring>
    {
        public override void Bake(CoordinatesAuthoring authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.None);
        }
    }
}


