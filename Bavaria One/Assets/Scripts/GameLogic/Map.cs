using System;
using UnityEngine;
using System.Reflection;

public struct Map{
    
    public Tile[,] tiles;

    public Map(int width, int height, float ResourceFrequency, int ResourceOctaves, float ResourceSeed) 
    {
        tiles = new Tile[width,height];
        //Generate Resource Grid
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (PerlinMultiOctave(i, j, ResourceFrequency, ResourceOctaves, ResourceSeed) < 0.4f)
                {
                    float v = UnityEngine.Random.Range(0.0f, 1.0f);
                    if (v < 0.2f)
                    {
                        tiles[i, j] = new Tile(new ResourceCount(0, 0, 0, 0, 0));
                    }
                    else if (v < 0.4f)
                    {
                        tiles[i, j] = new Tile(new ResourceCount(1, 0, 0, 0, 0));
                    }
                    else if (v < 0.6f)
                    {
                        tiles[i, j] = new Tile(new ResourceCount(0, 1, 0, 0, 0));
                    }
                    else if (v < 0.8f)
                    {
                        tiles[i, j] = new Tile(new ResourceCount(0, 0, 1, 0, 0));
                    }
                    else
                    {
                        tiles[i, j] = new Tile(new ResourceCount(0, 0, 0, 1, 0));
                    }
                }
                else
                {
                    tiles[i, j] = new Tile(new ResourceCount(0, 0, 0, 0, 0));
                }
            }
        }
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
        return Mathf.PerlinNoise(0.1257f + x * frequency + seed, 0.355f + z * frequency + seed) * amplitude;
    }
}
