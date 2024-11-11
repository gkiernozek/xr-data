using Unity.Entities;
using UnityEngine;

namespace XRData
{
    public class DraggableGraphPivot : MonoBehaviour
    {
	    private EntityManager entityManager;
	    private Entity graphPivotTransformDataEntity;
	    
		private void Awake()
		{
			entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
			
			graphPivotTransformDataEntity = entityManager.CreateEntity(typeof(GraphPivotTransformData));
			entityManager.SetComponentData(graphPivotTransformDataEntity, new GraphPivotTransformData{position = transform.position, rotation = transform.rotation, scale = transform.localScale});
	    }

        private void Update()
        {
	        entityManager.SetComponentData(graphPivotTransformDataEntity, new GraphPivotTransformData{position = transform.position, rotation = transform.rotation, scale = transform.localScale});
        }
    }
}
