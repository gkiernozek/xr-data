using Unity.Entities;
using UnityEngine;

namespace XRData
{
    public struct InteractionPoint : IComponentData
    {
    }
    
    class InteractionPointAuthoring : MonoBehaviour
    {
        class InteractionPointAuthoringBaker : Baker<InteractionPointAuthoring>
        {
            public override void Bake(InteractionPointAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new InteractionPoint());
            }
        }
    }
}
