using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GridRenderer : MonoBehaviour 
{
	public float GridSpacing;
	public float GridThickness;
	public Color GridColor;
	public float ConnectionThickness;
	public Color ConnectionColor;
	public Color PreviewColor;
	public Color PreviewOverlayColor;
	public Color BaseColor;

	private Texture2D builtConnections;
	private Texture2D previewConnections;
	private Material material;

	int width; 
	int height;

	void Awake()
	{
		MeshRenderer mr = GameObject.Find("Grid Object").GetComponent<MeshRenderer>();
        material = mr.materials[0];

		width = GameManager.Instance.width;
		height = GameManager.Instance.height;

		builtConnections = new Texture2D(width, height);
		previewConnections = new Texture2D(width, height);
		var fillColor = new Color(0, 0, 0, 0);
		var fillColorArray =  builtConnections.GetPixels();
		for(var i = 0; i < fillColorArray.Length; i++)
		{
			fillColorArray[i] = fillColor;
		}
		
		builtConnections.SetPixels(fillColorArray);
		previewConnections.SetPixels(fillColorArray);
		
		builtConnections.Apply();
		previewConnections.Apply();
	}

	void OnPreRender()
	{
		material.SetFloat("_GridSpacing", GridSpacing);
		material.SetTexture("_ConnectionTexture", builtConnections);
		material.SetTexture("_PreviewTexture", previewConnections);
		material.SetFloat("_GridThickness", GridThickness);
		material.SetColor("_GridColor", GridColor);
		material.SetFloat("_ConnectionThickness", ConnectionThickness);
		material.SetColor("_ConnectionColor", ConnectionColor);
		material.SetColor("_PreviewColor", PreviewColor);
		material.SetColor("_PreviewOverlayColor", PreviewOverlayColor);
		material.SetColor("_BaseColor", BaseColor);
	}

	public void AddConnectionToBuilt(Vector2 left, Vector2 right)
	{
		SetColorInAdjacentPixels(left, right, builtConnections, 1.0f);
	}

	public void RemoveConnectionFromBuilt(Vector2 left, Vector2 right)
	{
		SetColorInAdjacentPixels(left, right, builtConnections, 0.0f);
	}

	public void AddConnectionToPreview(Vector2 left, Vector2 right)
	{
		SetColorInAdjacentPixels(left, right, previewConnections, 1.0f);
	}

	public void RemoveConnectionFromPreview(Vector2 left, Vector2 right)
	{
		SetColorInAdjacentPixels(left, right, previewConnections, 0.0f);
	}

	public void SetColorInAdjacentPixels(Vector2 left, Vector2 right, Texture2D texture, float newValue) 
	{
		Vector2 point1 = left.x + left.y < right.x + right.y ? left : right;
		Vector2 point2 = left.x + left.y > right.x + right.y ? left : right;
		var delta = new Vector2(point2.x - point1.x, point2.y - point1.y);

		if(delta.magnitude != 1f)
			return;

		//Square 1
		int x1 = (int)(point1.x - delta.y);
		int y1 = (int)(point1.y - delta.x);
		Color color1 = texture.GetPixel(x1, y1);
		if(delta.x > 0)
			color1.r = newValue;
		else
			color1.g = newValue;
		texture.SetPixel(x1, y1, color1);
		
		//Square 2
		int x2 = (int)point1.x;
		int y2 = (int)point1.y;
		Color color2 = texture.GetPixel(x2, y2);
		if(delta.x > 0)
			color2.b = newValue;
		else
			color2.a = newValue;
		texture.SetPixel(x2, y2, color2);

		//Apply
		texture.Apply();
	}
}
