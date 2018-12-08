using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject TerrainPrefab;

    public int GridX;
    public int GridY;
    public float Spacing;

    void Start()
    {
        //Generate Terrain Meshes
        for (float x = -GridX; x < GridX; x += 1.0f)
        {
            for (float z = -GridY; z < GridY; z += 1.0f)
            {
                Instantiate(
                    TerrainPrefab,
                    new Vector3(x * Spacing + this.transform.position.x, 0.0f, z * Spacing + this.transform.position.z),
                    Quaternion.identity,
                    this.transform);
            }
        }
    }
}
