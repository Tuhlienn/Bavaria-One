using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class NoiseTerrain : MonoBehaviour 
{
	public int mapSize = 50;
	public int voxelsPerUnit = 10;
	public float amplitude = 5;
	public float frequency = 0.1f;
	public int octaves = 4;
	public float seed = 0;

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

		//Vertices
		vertices = new Vector3[vertexCount];

		int i = 0;

		for(int z = 0; z <= voxelCount; z++) 
		{
			for(int x = 0; x <= voxelCount; x++) 
			{
				float height = PerlinMultiOctave(x + (this.transform.position.x * voxelsPerUnit), z + (this.transform.position.z * voxelsPerUnit), frequency, octaves, seed) * amplitude;
				vertices[i] = new Vector3(x / (float)voxelsPerUnit, height, z / (float)voxelsPerUnit);
				i++;
			}
		}

		//Triangles
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

		/* 
		colors = new Color[vertexCount];
		for(i = 0; i < colors.Length; i++)
		{
			colors[i] = Color.red;
		} 
		*/
	}

	void UpdateMesh()
	{
		terrain.Clear();
		terrain.vertices = vertices;
		terrain.triangles = triangles;
		terrain.colors = colors;

		terrain.RecalculateNormals();
	}

	public static float PerlinMultiOctave(float x, float z, float frequency, int octaves, float seed) 
	{
		var result = Perlin(x, z, frequency, 1, seed);
		for(int i = 2; i <= octaves; i++) 
		{
			result += Perlin(x, z, frequency * i, 1f / i, seed) - (0.5f / i);
		}
		return Mathf.Clamp(result, 0, 1);
	}

	public static float Perlin(float x, float z, float frequency, float amplitude, float seed)
	{
		return Mathf.PerlinNoise(0.1f + x * frequency + seed, 0.1f + z * frequency + seed) * amplitude;
	}
}
