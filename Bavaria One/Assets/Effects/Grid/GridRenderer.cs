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
	public Color BaseColor;

	private Texture2D adjacencyTexture;
	private Material material;

	int width; 
	int height;

	void Awake()
	{
		MeshRenderer mr = GameObject.Find("Grid Object").GetComponent<MeshRenderer>();
        material = mr.materials[0];

		width = GameManager.Instance.width;
		height = GameManager.Instance.height;

		adjacencyTexture = new Texture2D(width, height);
		var fillColor = new Color(0, 0, 0, 0);
		var fillColorArray =  adjacencyTexture.GetPixels();
		for(var i = 0; i < fillColorArray.Length; i++)
		{
			fillColorArray[i] = fillColor;
		}
		
		adjacencyTexture.SetPixels(fillColorArray);
		
		adjacencyTexture.Apply();
	}

	void OnPreRender()
	{
		material.SetFloat("_GridSpacing", GridSpacing);
		material.SetTexture("_ConnectionTexture", adjacencyTexture);
		material.SetFloat("_GridThickness", GridThickness);
		material.SetColor("_GridColor", GridColor);
		material.SetFloat("_ConnectionThickness", ConnectionThickness);
		material.SetColor("_ConnectionColor", ConnectionColor);
		material.SetColor("_BaseColor", BaseColor);
	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(10, 10, 110, 110), adjacencyTexture, ScaleMode.ScaleToFit, true, 1.0F);
	}

	public void AddConnectionToTexture(Vector2 left, Vector2 right) 
	{
		Vector2 point1 = left.x + left.y < right.x + right.y ? left : right;
		Vector2 point2 = left.x + left.y > left.x + left.y ? left : right;
		var delta = new Vector2(point2.x - point1.x, point2.y - point1.y);

		if(delta.magnitude != 1f)
			return;

		//Square 1
		int x1 = (int)(point1.x - delta.y);
		int y1 = (int)(point1.y - delta.x);
		Color color1 = adjacencyTexture.GetPixel(x1, y1);
		if(delta.x > 0)
			color1.r = 1.0f;
		else
			color1.g = 1.0f;
		adjacencyTexture.SetPixel(x1, y1, color1);
		
		//Square 2
		int x2 = (int)point1.x;
		int y2 = (int)point1.y;
		Color color2 = adjacencyTexture.GetPixel(x2, y2);
		if(delta.x > 0)
			color2.b = 1.0f;
		else
			color2.a = 1.0f;
		adjacencyTexture.SetPixel(x2, y2, color2);

		//Apply
		adjacencyTexture.Apply();
	}

	/* 
	void Update()
	{
		for(int z = 0; z < height + 1; z++) 
		{
			for(int x = 0; x < width + 1; x++) 
			{
				Vector2 center = new Vector2(x - width / 2, z - height / 2);

				Color col = new Color();

				Connection top = GameManager.Instance.Connections.ConnectionAt(center, center + Vector2.up);
				col.r = (top == null) ? 0 : 1;

				Connection right = GameManager.Instance.Connections.ConnectionAt(center, center + Vector2.right);
				col.g = (right == null) ? 0 : 1;

				Connection bottom = GameManager.Instance.Connections.ConnectionAt(center, center + Vector2.down);
				col.b = (bottom == null) ? 0 : 1;

				Connection left = GameManager.Instance.Connections.ConnectionAt(center, center + Vector2.left);
				col.a = (left == null) ? 0 : 1;

				adjacencyTexture.SetPixel(x, z, col);
			}
		}
		adjacencyTexture.Apply();
	}
	*/
}
