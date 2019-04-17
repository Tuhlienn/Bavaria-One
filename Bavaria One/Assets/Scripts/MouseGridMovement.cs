using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseGridMovement : MonoBehaviour 
{
	protected enum HoverType { Point, Edge, Face }

	[HideInInspector] public City selectedCity;

	[SerializeField] protected Transform selectionObject;
	[SerializeField] protected float hoverPrecision = 0.35f;
	[SerializeField] protected Mesh pointMesh;
	[SerializeField] protected Mesh edgeMesh;
	[SerializeField] protected Mesh faceMesh;

	protected Vector3 hoveredPoint;
	protected bool xBetweenFullUnits;
	protected bool zBetweenFullUnits;
	protected HoverType hoverType;
	protected Vector2 connectionStart;
	protected bool connecting = false;
	protected List<Vector2> previewConnections;


	void Start() 
	{
		previewConnections = new List<Vector2>();
    }
	
	void Update() 
	{
		// stop if the cursor is hovering over a GUI-element
        if (EventSystem.current.IsPointerOverGameObject())
        {
            selectionObject.gameObject.SetActive(false);
            return;
        }

		HandleCursorPosition();

		if(selectionObject.gameObject.activeSelf)
		{
			HandleInput();
		}	
	}

	protected void HandleCursorPosition()
	{
		// get current hovered position on the XZ-plane
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Plane xzPlane = new Plane(Vector3.up, Vector3.zero);
		
		float distance;
		if (xzPlane.Raycast(ray, out distance)) 
		{
			// get the hit point:
			Vector3 temp = ray.GetPoint(distance);
			// and rounding it to half units
			hoveredPoint = new Vector3((int)Mathf.Round(temp.x * 2), 0, (int)Mathf.Round(temp.z * 2)) / 2.0f;

			// stop if there is no point nearby
			if ((hoveredPoint - temp).magnitude > hoverPrecision)
			{
				selectionObject.gameObject.SetActive(false);
				return;
			}

			xBetweenFullUnits = hoveredPoint.x % 1 != 0.0f;
			zBetweenFullUnits = hoveredPoint.z % 1 != 0.0f;
			var meshFilter = selectionObject.GetComponent<MeshFilter>();
			
			// hovering over point
			if(!xBetweenFullUnits && !zBetweenFullUnits)
			{
				meshFilter.mesh = pointMesh;
				hoverType = HoverType.Point;
			}
			// hovering over edge
			else if(!xBetweenFullUnits || !zBetweenFullUnits)
			{
				meshFilter.mesh = edgeMesh;
				hoverType = HoverType.Edge;
				selectionObject.transform.rotation = Quaternion.Euler(90.0f, xBetweenFullUnits ? 90.0f : 0.0f, 0);
			}
			// hovering over face
			else
			{
				meshFilter.mesh = faceMesh;
				hoverType = HoverType.Face;
			}

			// update selection object
			selectionObject.position = hoveredPoint;
			selectionObject.gameObject.SetActive(true);
		}
	}

	protected void HandleInput()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Vector2 clicked = new Vector2(hoveredPoint.x, hoveredPoint.z);

			switch(hoverType)
			{
				case HoverType.Point:
					ClickedPoint(clicked);
					break;
				case HoverType.Edge:
					ClickedEdge(clicked);
					break;
				case HoverType.Face:
					ClickedFace(clicked);
					break;
			}
		}

		if(Input.GetMouseButton(0))
		{
			if(hoverType == HoverType.Point && connecting)
			{
				UpdateConnectionPreview();
			}
		}

		if(Input.GetMouseButtonUp(0))
		{
			if(connecting)
			{
				connecting = false;
				OperateOnPreviewConnections(BuildAndRemoveFromPreview);
				previewConnections = new List<Vector2>();
			}
		}
	}

	protected void ClickedPoint(Vector2 clicked)
	{
		//ConnectionBuilder Mode
		if(ButtonManager.Instance.ConnectionBuilderMode)
		{
			connectionStart = clicked;
			connecting = true;
		}
		//CityBuilder Mode
		else if(ButtonManager.Instance.CityBuilderMode)
		{
			selectedCity = GameManager.GetCity(clicked);
			ButtonManager.Instance.UpgradePosition = hoveredPoint;
			ButtonManager.Instance.BuildCity();
		}
		//Normal Mode
		else 
		{
			selectedCity = GameManager.GetCity(clicked);
			ButtonManager.Instance.showPopup(hoveredPoint);
		}
	}
	
	protected void ClickedEdge(Vector2 clicked)
	{
		if(ButtonManager.Instance.CityBuilderMode)
		{
			return;
		}

		Vector2 left;
		Vector2 right;
		if(xBetweenFullUnits)
		{
			left = new Vector2(hoveredPoint.x - 0.5f, hoveredPoint.z);
			right = new Vector2(hoveredPoint.x + 0.5f, hoveredPoint.z);
		}
		else
		{
			left = new Vector2(hoveredPoint.x, hoveredPoint.z - 0.5f);
			right = new Vector2(hoveredPoint.x, hoveredPoint.z + 0.5f);
		}
		
		CityView.Instance.BuildConnection(left, right, false);
	}

	protected void ClickedFace(Vector2 clicked)
	{
		if(ButtonManager.Instance.CityBuilderButton)
		{
			var pos = new Vector2(hoveredPoint.x - 0.5f + GameManager.Instance.width / 2f, 
								  hoveredPoint.y - 0.5f + GameManager.Instance.height / 2f);
			CityView.Instance.BuildEnergyPanel(pos);
		}
	}

	protected void UpdateConnectionPreview()
	{
		OperateOnPreviewConnections(RemoveFromPreview);

		var path = GameManager.Instance.Connections.AStar(connectionStart, new Vector2(hoveredPoint.x, hoveredPoint.z));
		if(path != null && path.Count >= 2)
		{
			previewConnections = path;
			OperateOnPreviewConnections(AddToPreview);
		}
	}

	//Helper Functions
	protected void OperateOnPreviewConnections(Action<Vector2, Vector2> operation)
	{
		for(int i = 0; i < previewConnections.Count - 1; i++)
		{
			var left = previewConnections[i];
			var right = previewConnections[i + 1];
			operation(left, right);
		}
	}
	protected void RemoveFromPreview(Vector2 left, Vector2 right)
	{
		GridRenderer.Instance.RemoveConnectionFromPreview(left, right);
	}
	protected void AddToPreview(Vector2 left, Vector2 right)
	{
		GridRenderer.Instance.AddConnectionToPreview(left, right);
	}
	protected void BuildAndRemoveFromPreview(Vector2 left, Vector2 right)
	{
		GridRenderer.Instance.RemoveConnectionFromPreview(left, right);
		CityView.Instance.BuildConnection(left, right, false);
	}
}
