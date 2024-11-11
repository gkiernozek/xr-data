using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace XRData
{
    public class XRInteractionPoints : MonoBehaviour
    {
        public Transform[] xrInteractionPoints;
        
        private NativeList<float3> interactionPositions { get; set; }

        private EntityManager entityManager;
        private Entity interactionPointsDataEntity;

        private void Awake()
        {
            interactionPositions = new NativeList<float3>(Allocator.Persistent);
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            interactionPointsDataEntity = entityManager.CreateEntity(typeof(XRInteractionPointsDataComponent));
            entityManager.SetComponentData(interactionPointsDataEntity, new XRInteractionPointsDataComponent { XRInteractionPositions = interactionPositions });
        }

        private void Update()
        {
            UpdateXRInteractionPointsDataComponent();
        }

        private void UpdateXRInteractionPointsDataComponent()
        {
            interactionPositions.Clear();

            var xrInteractionPointsData = entityManager.GetComponentData<XRInteractionPointsDataComponent>(interactionPointsDataEntity);
            xrInteractionPointsData.XRInteractionPositions.Clear();
            
            foreach (var interactionPoint in xrInteractionPoints)
            {
                if (interactionPoint.gameObject.activeInHierarchy)
                {
                    xrInteractionPointsData.XRInteractionPositions.Add(interactionPoint.position);
                }
            }

            entityManager.SetComponentData(interactionPointsDataEntity, xrInteractionPointsData);
        }

        private void OnDestroy()
        {
            interactionPositions.Dispose();
        }
    }
}
