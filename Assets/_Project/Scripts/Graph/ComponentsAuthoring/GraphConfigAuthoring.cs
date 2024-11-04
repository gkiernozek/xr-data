using Unity.Entities;
using UnityEngine;

namespace XRData
{
    public struct GraphConfig : IComponentData
    {
        public float dimensionSize;
        public float spacing;
        public Entity pointPrefab;
        public Entity graphPointsTransform;
    }

    internal class GraphConfigAuthoring : MonoBehaviour
    {
        public float dimensionSize;
        public float spacing;
        public GameObject pointPrefab;
        public GameObject graphPointsTransform;

        private class GraphConfigAuthoringBaker : Baker<GraphConfigAuthoring>
        {
            public override void Bake(GraphConfigAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.None);
            
                AddComponent(entity, new GraphConfig
                {
                    dimensionSize = authoring.dimensionSize,
                    spacing = authoring.spacing,
                    pointPrefab = GetEntity(authoring.pointPrefab, TransformUsageFlags.Dynamic),
                    graphPointsTransform = GetEntity(authoring.graphPointsTransform, TransformUsageFlags.Dynamic)
                });
            }
        }
    }
}