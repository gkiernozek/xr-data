using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace XRData
{
    public partial class PointsSpawnSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<GraphConfig>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            Enabled = false;
        
            var config = GetSingleton<GraphConfig>();
        
            for (int i = 0; i < config.dimensionSize; i++)
            {
                for (int j = 0; j < config.dimensionSize; j++)
                {
                    var point = EntityManager.Instantiate(config.pointPrefab);
                    EntityManager.AddComponentData(point, new Parent
                    {
                        Value = config.graphPointsTransform
                    });
                    EntityManager.SetComponentData(point, new LocalTransform
                    {
                        Position = new float3(i * config.spacing, 0, j * config.spacing),
                        Rotation = quaternion.identity,
                        Scale = 1f
                    });
                    EntityManager.AddComponentData(point, new Coordinates
                    {
                        xz = new int2(i, j)
                    });
                }
            }
        }
    }
}