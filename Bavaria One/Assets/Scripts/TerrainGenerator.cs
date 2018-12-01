using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
    public GameObject TerrainPrefab;
    public int GridX;
    public int GridY;
    public float Spacing;

    public int MeshMapSize = 25;
    public int MeshVoxelsPerUnit = 10;
    public float MeshAmplitude = 0.5f;
    public float MeshFrequency = 1.0f;
    public int MeshOctaves = 5;
    public float MeshSeed = 54598.0f;

    public float ResourceLowFrequency = 0.1f;
    public int ResourceLowOctaves = 5;
    public float ResourceLowSeed = 111.68465165f;

    public float ResourceHighFrequency = 0.5f;
    public int ResourceHighOctaves = 6;
    public float ResourceHighSeed = -64654.974654f;

    public enum Resources { Leer, Geld, Bier, Stahl, Beton, Strom };

    public Resources[,] ResourceGrid;

	// Use this for initialization
	void Start () {
        //Generate Terrain Meshes
		for(float x = - GridX; x < GridX; x += 1.0f)
        {
            for(float z = -GridY; z < GridY; z += 1.0f)
            {
                Instantiate(TerrainPrefab,
                    new Vector3(x * Spacing + this.transform.position.x, 0.0f, z * Spacing + this.transform.position.z),
                    Quaternion.identity,
                    this.transform);
            }
        }
        //Generate Resource Grid
        ResourceGrid = new Resources[50 * GridX, 50 * GridY];
        for (int i = 0; i < 100; i++){
            for (int j = 0; j < 100; j++)
            {
                if (0.4f > PerlinMultiOctave(i, j, ResourceLowFrequency, ResourceLowOctaves, ResourceLowSeed)){
                    float v = PerlinMultiOctave(i, j, ResourceHighFrequency, ResourceHighOctaves, ResourceHighSeed);
                    if (v < 0.2f)
                    {
                        ResourceGrid[i, j] = Resources.Leer;
                    }
                    else if (v < 0.4f)
                    {
                        ResourceGrid[i, j] = Resources.Geld;
                    }
                    else if (v < 0.6f)
                    {
                        ResourceGrid[i, j] = Resources.Bier;
                    }
                    else if (v < 0.8f)
                    {
                        ResourceGrid[i, j] = Resources.Stahl;
                    }
                    else
                    {
                        ResourceGrid[i, j] = Resources.Beton;
                    }
                }
                else {
                    ResourceGrid[i, j] = Resources.Leer;
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public static float PerlinMultiOctave(float x, float z, float frequency, int octaves, float seed)
    {
        var result = Perlin(x, z, frequency, 1, seed);
        for (int i = 2; i <= octaves; i++)
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
