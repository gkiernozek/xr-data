using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace XRData
{
    public partial struct GraphInteractionSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Coordinates>();
            state.RequireForUpdate<GraphConfig>();
            state.RequireForUpdate<XRInteractionPointsData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var graphConfig = SystemAPI.GetSingleton<GraphConfig>();
            var graphPointsTransform = graphConfig.graphPointsTransform;
            var graphPointsTransformMatrix = SystemAPI.GetComponent<LocalToWorld>(graphPointsTransform).Value;

            var xrInteractionPointsData = SystemAPI.GetSingleton<XRInteractionPointsData>();
            var interactionPointPositions = xrInteractionPointsData.XRInteractionPositions;

            for (var i = 0; i < interactionPointPositions.Length; i++)
            {
                interactionPointPositions[i] = math.transform(math.inverse(graphPointsTransformMatrix), interactionPointPositions[i]);
            }

            var interactionUpdateJob = new InteractionUpdateJob
            {
                interactionPointPositions = interactionPointPositions
            };
                
            interactionUpdateJob.ScheduleParallel(state.Dependency).Complete();
        }
        
        [BurstCompile]
        public partial struct InteractionUpdateJob : IJobEntity
        {
            [ReadOnly] public NativeList<float3> interactionPointPositions;

            private void Execute(ref LocalTransform localTransform, in Coordinates coordinates)
            {
                var entityPosition = localTransform.Position;
                var minDistance = float.MaxValue;

                // Graph point scale is based on the distance to the closest interaction point
                foreach (var interactionPoint in interactionPointPositions)
                {
                    var distance = math.distance(entityPosition, interactionPoint);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                    }
                }

                var scale = 1.0f / (1.0f + math.pow(minDistance*2, 3));
                scale = math.clamp(scale, 0.15f, 0.6f);
                localTransform.Scale = scale;
            }
        }
    }
}