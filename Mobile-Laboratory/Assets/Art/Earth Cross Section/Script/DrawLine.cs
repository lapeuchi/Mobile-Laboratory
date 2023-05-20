using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrawLine : MonoBehaviour
{
	
	
	    public Transform cam;
    	public GameObject startPoint;
		public GameObject target;
		public float LineWidth;
        private LineRenderer line;
		public Material lineMaterial;
		public Transform textMeshTransform;
	   

    void Start()
    {
		
		
	
       // LineRenderer  lineRenderer = GetComponent<LineRenderer> ();
		
		
    }
	
	void Update ()
	
  {
	    line = this.gameObject.GetComponent<LineRenderer>();
		line.SetVertexCount(2);
		line.material = lineMaterial;
		
		
		
		line.SetWidth(LineWidth, LineWidth);
		line.SetPosition (0, startPoint.transform.position);
        line.SetPosition (1, target.transform.position);
		

  } 
  
}

