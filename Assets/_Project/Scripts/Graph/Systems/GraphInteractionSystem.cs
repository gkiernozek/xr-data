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
            state.RequireForUpdate<InteractionPoint>();
            state.RequireForUpdate<Coordinates>();
            state.RequireForUpdate<GraphConfig>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var graphConfig = SystemAPI.GetSingleton<GraphConfig>();
            var graphPointsTransform = graphConfig.graphPointsTransform;
            float4x4 graphPointsTransformMatrix = SystemAPI.GetComponent<LocalToWorld>(graphPointsTransform).Value;

            
            NativeList<float3> interactionPointPositions = new NativeList<float3>(Allocator.TempJob);
            
            foreach((RefRO<InteractionPoint> _, RefRO<LocalTransform> localTransform) in SystemAPI.Query<RefRO<InteractionPoint>,RefRO<LocalTransform>>())
            {
                float3 localInteractionPointPosition = math.transform(math.inverse(graphPointsTransformMatrix), localTransform.ValueRO.Position);

                interactionPointPositions.Add(localInteractionPointPosition);
            }
            
            InteractionUpdateJob interactionUpdateJob = new InteractionUpdateJob
            {
                interactionPointPositions = interactionPointPositions
            };
                
            interactionUpdateJob.ScheduleParallel(state.Dependency).Complete();
            interactionPointPositions.Dispose();
        }
        
        [BurstCompile]
        public partial struct InteractionUpdateJob : IJobEntity
        {
            [ReadOnly] public NativeList<float3> interactionPointPositions;
            
            public void Execute(ref LocalTransform localTransform, in Coordinates coordinates)
            {
                float3 entityPosition = localTransform.Position;
                float minDistance = float.MaxValue;

                // Scale based on the distance to the closest interaction point
                foreach (float3 interactionPoint in interactionPointPositions)
                {
                    float distance = math.distance(entityPosition, interactionPoint);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                    }
                }

                float scale = 2.0f / (1.0f + minDistance*minDistance);
                scale = math.clamp(scale, 0.25f, 1.0f);
                localTransform.Scale = scale;
            }
        }
    }
}