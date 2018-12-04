﻿using System.Collections;
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
	public City selectedCity;
	private bool xBetweenPoints;
	private bool zBetweenPoints;
	private int hoverType; //0 = edge, 1 = point, 2 = face

    public AudioClip railSound;


    void Awake () 
	{
		cityManager = GameObject.Find("CityManager").GetComponent<CityView>();
    }
	
	void Update () 
	{
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

			xBetweenPoints = hoveredPoint.x % 1 != 0.0f;
			zBetweenPoints = hoveredPoint.z % 1 != 0.0f;

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
				if(hoverType == 1)
				{
					var lvl = 0;
					var cost = 2;
					selectedCity = GameManager.GetCity(new Vector2(hoveredPoint.x, hoveredPoint.z));
					if(selectedCity != null) 
					{
						lvl = selectedCity.upgradeLevel;
						cost = cost + lvl;
					}
					buttonManager.showPopup(hoveredPoint, lvl, cost);
				}
				if(hoverType == 2)
				{
					//buttonManager.showPopup(hoveredPoint);
				}
			}
			else
			{
				if(hoverType == 0) 
				{
					Vector2 left;
					Vector2 right;
					if(xBetweenPoints)
					{
						left = new Vector2(hoveredPoint.x - 0.5f, hoveredPoint.z);
						right = new Vector2(hoveredPoint.x + 0.5f, hoveredPoint.z);
					}
					else
					{
						left = new Vector2(hoveredPoint.x, hoveredPoint.z - 0.5f);
						right = new Vector2(hoveredPoint.x, hoveredPoint.z + 0.5f);
					}
					cityManager.BuildConnection(left, right, false);
                    SoundManager.Instance.Play(railSound);
				}
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
