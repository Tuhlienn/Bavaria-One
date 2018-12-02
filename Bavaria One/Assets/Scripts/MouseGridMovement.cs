using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseGridMovement : MonoBehaviour {

	public Transform Selection;
	public float hoverPrecision = 0.35f;
	public Mesh[] meshes;

    public bool selectMode = true;
    public ButtonManager buttonManager;

	private CityView cityManager;

	private Vector3 hoveredPoint;
	private int hoverType;


    void Awake () 
	{
		cityManager = GameObject.Find("CityManager").GetComponent<CityView>();
    }
	
	void Update () 
	{
        if (!selectMode)
            return;

        if (EventSystem.current.IsPointerOverGameObject())
        {
            Selection.gameObject.SetActive(false);
            return;
        }


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Plane xzPlane = new Plane(Vector3.up, Vector3.zero);
		
		float distance;
		if (xzPlane.Raycast(ray, out distance)) 
		{
			// get the hit point:
			Vector3 temp = ray.GetPoint(distance);
			hoveredPoint = new Vector3((int)Mathf.Round(temp.x * 2), 0, (int)Mathf.Round(temp.z * 2)) / 2.0f;

			bool xBetweenPoints = hoveredPoint.x % 1 != 0.0f;
			bool zBetweenPoints = hoveredPoint.z % 1 != 0.0f;

			if(xBetweenPoints && zBetweenPoints)
			{
                if ((hoveredPoint - temp).magnitude < hoverPrecision)
                {
                    var meshFilter = Selection.GetComponent<MeshFilter>();
                    meshFilter.mesh = meshes[2];
					hoverType = 2;
                    Selection.position = hoveredPoint;
                    Selection.gameObject.SetActive(true);
                }
                else
                {
                    Selection.gameObject.SetActive(false);
                }
			}
			else 
			{
				if((hoveredPoint - temp).magnitude < hoverPrecision)
				{
					var meshFilter = Selection.GetComponent<MeshFilter>();
					if(!(xBetweenPoints || zBetweenPoints))
					{
						meshFilter.mesh = meshes[1];
						hoverType = 1;
					}
					else 
					{
						meshFilter.mesh = meshes[0];
						hoverType = 0;
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

		if(Input.GetMouseButtonDown(0) && Selection.gameObject.activeSelf) 
		{
			if(selectMode) 
			{
				buttonManager.showPopup(hoveredPoint);
			}
			else 
			{
				
			}
		}
	}

    public void ToggleSelectMode()
    {
        selectMode = !selectMode;

        if (!selectMode)
        {
			Selection.gameObject.SetActive(false);
			buttonManager.popUpUpgrade.SetActive(false);
        }
    }
}
