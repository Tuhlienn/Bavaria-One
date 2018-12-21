using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseGridMovement : MonoBehaviour {

	public Transform Selection;
	public float hoverPrecision = 0.35f;
	public Mesh[] meshes;

    public ButtonManager buttonManager;

	private CityView cityManager;
	private GridRenderer Grid;

	private Vector3 hoveredPoint;
	public City selectedCity;
	private bool xBetweenPoints;
	private bool zBetweenPoints;
	private int hoverType; //0 = point, 1 = edge, 2 = face
	private Vector2 connectionStart;
	private bool connecting = false;
	private List<Vector2> previewConnections;

    public AudioClip railSound;


    void Awake () 
	{
		cityManager = GameObject.Find("CityManager").GetComponent<CityView>();
        Grid = Camera.main.GetComponent<GridRenderer>();
		previewConnections = new List<Vector2>();
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

			//No point nearby
			if ((hoveredPoint - temp).magnitude > hoverPrecision)
			{
				Selection.gameObject.SetActive(false);
				return;
			}

			xBetweenPoints = hoveredPoint.x % 1 != 0.0f;
			zBetweenPoints = hoveredPoint.z % 1 != 0.0f;
			var meshFilter = Selection.GetComponent<MeshFilter>();
			
			//Hovering over point
			if(!xBetweenPoints && !zBetweenPoints)
			{
				meshFilter.mesh = meshes[0];
				hoverType = 0;
			}
			//Hovering over edge
			else if(!xBetweenPoints || !zBetweenPoints)
			{
				meshFilter.mesh = meshes[1];
				hoverType = 1;
				Selection.transform.rotation = Quaternion.Euler(90.0f, xBetweenPoints ? 90.0f : 0.0f, 0);
			}
			//Hovering over face
			else
			{
				meshFilter.mesh = meshes[2];
				hoverType = 2;
			}

			Selection.position = hoveredPoint;
			Selection.gameObject.SetActive(true);
		}

		//Handle Input
		if(Selection.gameObject.activeSelf)
		{
			if(Input.GetMouseButtonDown(0))
			{
				Vector2 clicked = new Vector2(hoveredPoint.x, hoveredPoint.z);

				if(hoverType == 0)
				{
					//ConnectionBuilder Mode
					if(buttonManager.ConnectionBuilderMode)
					{
						connectionStart = clicked;
						connecting = true;
						return;
					}

					//CityBuilder Mode
					if(buttonManager.CityBuilderMode)
					{
						selectedCity = GameManager.GetCity(clicked);
						buttonManager.UpgradePosition = hoveredPoint;
						buttonManager.BuildCity();
					}
					//Normal Mode
					else 
					{
						selectedCity = GameManager.GetCity(clicked);
						buttonManager.showPopup(hoveredPoint);
					}
				}
				else if(hoverType == 1) 
				{
					if(buttonManager.CityBuilderMode)
					{
						return;
					}

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
					if(cityManager.BuildConnection(left, right, false))
					{
						SoundManager.Instance.Play(railSound);
					}
				}
				else if(hoverType == 2)
				{
					if(buttonManager.CityBuilderButton)
					{
						var pos = new Vector2(hoveredPoint.x - 0.5f + GameManager.Instance.width / 2, hoveredPoint.y - 0.5f + GameManager.Instance.height / 2);
						//GameManager.BuildEnergyPanel(pos);
					}
				}
			}

			if(Input.GetMouseButton(0))
			{
				if(hoverType == 0 && connecting)
				{
					for(int i = 0; i < previewConnections.Count - 1; i++)
					{
						var left = previewConnections[i];
						var right = previewConnections[i + 1];
						Grid.RemoveConnectionFromPreview(left, right);
					}

					var path = GameManager.Instance.Connections.AStar(connectionStart, new Vector2(hoveredPoint.x, hoveredPoint.z));
					if(path != null && path.Count >= 2)
					{
						previewConnections = path;

						for(int i = 0; i < previewConnections.Count - 1; i++)
						{
							var left = previewConnections[i];
							var right = previewConnections[i + 1];
							Grid.AddConnectionToPreview(left, right);
						}
					}
				}
			}

			if(Input.GetMouseButtonUp(0))
			{
				if(connecting)
				{
					connecting = false;
					
					for(int i = 0; i < previewConnections.Count - 1; i++)
					{
						var left = previewConnections[i];
						var right = previewConnections[i + 1];
						Grid.RemoveConnectionFromPreview(left, right);
						cityManager.BuildConnection(left, right, false);
					}

					previewConnections = new List<Vector2>();
				}
			}
		}
		
	}
}
