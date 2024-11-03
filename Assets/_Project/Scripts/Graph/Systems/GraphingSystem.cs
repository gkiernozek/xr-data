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
            state.RequireForUpdate<Coordinates>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var config = SystemAPI.GetSingleton<GraphConfig>();

            GraphUpdateJob graphUpdateJob = new GraphUpdateJob
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

            public void Execute(ref LocalTransform localTransform, in Coordinates coordinates)
            {
                localTransform.Position =
                    new float3(
                        coordinates.xz[0] * spacing, 
                        0.5f+math.sin(time + coordinates.xz[0] * spacing + coordinates.xz[1] * spacing), 
                        coordinates.xz[1] * spacing);
            }
        }
    }
}
