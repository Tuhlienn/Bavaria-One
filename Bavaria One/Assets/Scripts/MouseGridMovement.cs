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
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit, 100.0f)) 
		{
			// get the hit point:
			Vector3 temp = hit.point;
			var hoveredPoint = new Vector3Int((int)Mathf.Round(temp.x), 1, (int)Mathf.Round(temp.z));

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
