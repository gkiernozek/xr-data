using System;
using System.Collections.Generic;
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
        private Entity interactionPointsEntity;

        private void Awake()
        {
            interactionPositions = new NativeList<float3>(Allocator.Persistent);
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            interactionPointsEntity = entityManager.CreateEntity(typeof(XRInteractionPointsData));
            entityManager.SetComponentData(interactionPointsEntity, new XRInteractionPointsData { XRInteractionPositions = interactionPositions });
        }

        private void Update()
        {
            interactionPositions.Clear();

            var xrInteractionPointsData = entityManager.GetComponentData<XRInteractionPointsData>(interactionPointsEntity);
            xrInteractionPointsData.XRInteractionPositions.Clear();
            
            foreach (var interactionPoint in xrInteractionPoints)
            {
                if (interactionPoint.gameObject.activeInHierarchy)
                {
                    xrInteractionPointsData.XRInteractionPositions.Add(interactionPoint.position);
                }
            }

            entityManager.SetComponentData(interactionPointsEntity, xrInteractionPointsData);
        }
        
        private void OnDestroy()
        {
            interactionPositions.Dispose();
        }
    }
}
