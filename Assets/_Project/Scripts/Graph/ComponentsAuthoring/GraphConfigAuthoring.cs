using System;
using Unity.Entities;
using UnityEngine;

namespace XRData
{
    public struct GraphConfig : IComponentData
    {
        public float size;
        public float spacing;
        public Entity pointPrefab;

        public override bool Equals(object obj)
        {
            return obj is GraphConfig other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(size, spacing, pointPrefab);
        }
    }

    class GraphConfigAuthoring : MonoBehaviour
    {
        public float size;
        public float spacing;
        public GameObject pointPrefab;
    
        class GraphConfigAuthoringBaker : Baker<GraphConfigAuthoring>
        {
            public override void Bake(GraphConfigAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.None);
            
                AddComponent(entity, new GraphConfig
                {
                    size = authoring.size,
                    spacing = authoring.spacing,
                    pointPrefab = GetEntity(authoring.pointPrefab, TransformUsageFlags.Dynamic)
                });
            }
        }
    }
}