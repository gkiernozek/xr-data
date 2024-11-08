using Unity.Burst;
using Unity.Entities;

namespace XRData
{
    public partial struct GraphPositioningSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var graphConfig = SystemAPI.GetSingleton<GraphConfig>();
            var graphPointsTransform = graphConfig.graphPointsTransform;
            
            
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}