using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class NoiseTerrain : MonoBehaviour 
{
	public int mapSize = 50;
	public int voxelsPerUnit = 10;
	public float amplitude = 5.0f;

	Vector3[] vertices;
	int[] triangles;
	Color[] colors;
	Mesh terrain;

	// Use this for initialization
	void Start () 
	{
		terrain = new Mesh();
		GetComponent<MeshFilter>().mesh = terrain;

		GenerateTerrain();
		UpdateMesh();
	}

	void GenerateTerrain() 
	{
		int voxelCount = mapSize * voxelsPerUnit;
		int vertexCount = (voxelCount + 1) * (voxelCount + 1);

		vertices = new Vector3[vertexCount];
		int i = 0;
		for(int z = 0; z <= voxelCount; z++) 
		{
			for(int x = 0; x <= voxelCount; x++) 
			{
				float height = Mathf.PerlinNoise(x / 100.0f, z / 100.0f) * amplitude;
				vertices[i] = new Vector3(x / (float)voxelsPerUnit, height, z / (float)voxelsPerUnit);
				i++;
			}
		}

		colors = new Color[vertexCount];
		for(i = 0; i < colors.Length; i++)
		{
			colors[i] = Color.red;
		}
		
		triangles = new int[voxelCount * voxelCount * 6];
		int vert = 0;
		int tris = 0;
		for(int z = 0; z < voxelCount; z++)
		{
			for(int x = 0; x < voxelCount; x++) 
			{
				triangles[tris + 0] = vert + 0;
				triangles[tris + 1] = vert + voxelCount + 1;
				triangles[tris + 2] = vert + 1;
				triangles[tris + 3] = vert + 1;
				triangles[tris + 4] = vert + voxelCount + 1;
				triangles[tris + 5] = vert + voxelCount + 2;

				vert++;
				tris += 6;
			}
			vert++;
		}

	}

	void UpdateMesh()
	{
		terrain.Clear();
		terrain.vertices = vertices;
		terrain.triangles = triangles;
		terrain.colors = colors;

		terrain.RecalculateNormals();
	}

	public static Texture2D AddWithOffset(Texture2D texA, float offsetA, Texture2D texB, float offsetB) 
	{
		int resX = texA.width;
		int resY = texA.height;

		Texture2D result = new Texture2D(resX, resY);
		result.filterMode = FilterMode.Point;

		if(resX != texB.width || resY != texB.height)
		{
			return result;
		}

		for (int y = 0; y < resY; y++)
		{
			for (int x = 0; x < resX; x++)
			{
				float[] rgb = new float[3];

				rgb[0] = texA.GetPixel(x,y).r + offsetA + texB.GetPixel(x,y).r + offsetB;
				rgb[1] = texA.GetPixel(x,y).g + offsetA + texB.GetPixel(x,y).g + offsetB;
				rgb[2] = texA.GetPixel(x,y).b + offsetA + texB.GetPixel(x,y).b + offsetB;

				result.SetPixel(x, y, new Color(rgb[0], rgb[1], rgb[2]));
			}
		}
		
		result.Apply();

		return result;
	}
}
