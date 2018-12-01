using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseGridMovement : MonoBehaviour {

	public Transform Selection;
	public float hoverPrecision = 0.35f;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Plane hPlane = new Plane(Vector3.up, Vector3.zero);
		float distance = 0; 
		if (hPlane.Raycast(ray, out distance)) 
		{
			// get the hit point:
			Vector3 temp = ray.GetPoint(distance);
			var hoveredPoint = new Vector3Int((int)Mathf.Round(temp.x), 0, (int)Mathf.Round(temp.z));

			if((hoveredPoint - temp).magnitude < hoverPrecision)
			{
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
