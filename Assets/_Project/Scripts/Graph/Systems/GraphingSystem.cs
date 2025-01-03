using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace XRData
{
    partial struct GraphingSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GraphConfig>();
            state.RequireForUpdate<CoordinatesComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var config = SystemAPI.GetSingleton<GraphConfig>();

            var graphUpdateJob = new GraphUpdateJob
            {
                time = (float)SystemAPI.Time.ElapsedTime,
                spacing = config.spacing
            };
            graphUpdateJob.ScheduleParallel();
        }

        [BurstCompile]
        public partial struct GraphUpdateJob : IJobEntity
        {
            public float time;
            public float spacing;

            private void Execute(ref LocalTransform localTransform, in CoordinatesComponent coordinatesComponent)
            {
                localTransform.Position =
                    new float3(
                        coordinatesComponent.xz[0] * spacing + spacing/2, 
                        //sine wave slightly rotated with offset above Y axis
                        1f+0.77f*math.sin(time - coordinatesComponent.xz[0] * spacing - coordinatesComponent.xz[1] * spacing/3), 
                        coordinatesComponent.xz[1] * spacing + spacing/2);
            }
        }
    }
}
