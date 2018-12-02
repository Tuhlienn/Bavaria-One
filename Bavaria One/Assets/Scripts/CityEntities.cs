using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityEntities : MonoBehaviour {
    public GameObject[,] Cities;
    public GameObject CityPrefab;
    // Use this for initialization
    void Start () {
        int width = GameManager.Instance.width;
        int height = GameManager.Instance.height;
        this.Cities = new GameObject[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Cities[i, j] = null;
            }
        }
    }
    
    // Update is called once per frame
    void Update () {
        
    }
    public void AddCity(Vector2 position)
    {

    }
    public void UpgradeCity(Vector2 position)
    {

    }
}