using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpawnNewspaper : MonoBehaviour 
{
    public Sprite[] images;
    public GameObject NewspaperPrefab;
    int i;
    GameObject currentNewspaper;
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.F9) )
        {
            InstantiateNewspaper();
        }
    }

    public void InstantiateNewspaper() 
    {
        if (i < images.Length)
        {
            currentNewspaper = Instantiate(NewspaperPrefab, transform);
            currentNewspaper.GetComponent<Image>().sprite = images[i];
            i++;
        }
    }
}
