using Unity.Entities;
using UnityEngine;

namespace XRData
{
    public class DraggableGraphPivot : MonoBehaviour
    {
	    [SerializeField] private Material graphPointMaterial;
	    
	    private static readonly int MaterialOriginProp = Shader.PropertyToID("_Origin");
	    private static readonly int MaterialSpreadProp = Shader.PropertyToID("_Spread");

	    
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
	        graphPointMaterial.SetFloat(MaterialOriginProp, transform.position.y); 
	        graphPointMaterial.SetFloat(MaterialSpreadProp, transform.localScale.y*0.125f/0.0825f); //0.0825 is a base draggable graph object scale, 0.125 is a base spread
        }
    }
}
