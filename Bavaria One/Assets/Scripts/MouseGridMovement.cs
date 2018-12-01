using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseGridMovement : MonoBehaviour {

	public Transform Selection;
	public float hoverPrecision = 0.35f;
	public Mesh[] meshes;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Plane xzPlane = new Plane(Vector3.up, Vector3.zero);
		
		float distance;
		if (xzPlane.Raycast(ray, out distance)) 
		{
			// get the hit point:
			Vector3 temp = ray.GetPoint(distance);
			var hoveredPoint = new Vector3((int)Mathf.Round(temp.x * 2), 0, (int)Mathf.Round(temp.z * 2)) / 2.0f;

			bool xBetweenPoints = hoveredPoint.x % 1 != 0.0f;
			bool zBetweenPoints = hoveredPoint.z % 1 != 0.0f;

			if(xBetweenPoints && zBetweenPoints)
			{
				Selection.gameObject.SetActive(false);
			}
			else 
			{
				if((hoveredPoint - temp).magnitude < hoverPrecision)
				{
					var meshFilter = Selection.GetComponent<MeshFilter>();
					if(!(xBetweenPoints || zBetweenPoints))
					{
						meshFilter.mesh = meshes[1];
					}
					else 
					{
						meshFilter.mesh = meshes[0];
						if(xBetweenPoints) 
						{
							Selection.transform.rotation = Quaternion.Euler(90.0f, 90.0f, 0);
						}
						else if(zBetweenPoints) 
						{
							Selection.transform.rotation = Quaternion.Euler(90.0f, 0, 0);
						}
					}

					Selection.position = hoveredPoint;
					Selection.gameObject.SetActive(true);
				}
				else 
				{
					Selection.gameObject.SetActive(false);
				}
			}
		}
	}
}
