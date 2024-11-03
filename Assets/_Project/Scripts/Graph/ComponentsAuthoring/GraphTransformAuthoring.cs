using Unity.Entities;
using UnityEngine;

namespace XRData
{
    public struct GraphTransform : IComponentData
    {
        public Entity transform;
    }

    class GraphTransformAuthoring : MonoBehaviour
    {
        class GraphTransformAuthoringBaker : Baker<GraphTransformAuthoring>
        {
            public override void Bake(GraphTransformAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.None);
            
                AddComponent(entity, new GraphTransform
                {
                    transform = GetEntity(authoring.transform, TransformUsageFlags.Dynamic)
                });
            }
        }
    }
}