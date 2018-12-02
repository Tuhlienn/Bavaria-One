﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpawnNewspaper : MonoBehaviour {
    
    public Sprite[] images;
    public GameObject NewspaperPrefab;
    public Transform parent;
    int i;
    GameObject currentNewspaper;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.F9) )
        {
            InstantiateNewspaper();
        }
       
        
    }

    public void InstantiateNewspaper() {
        if (i < images.Length)
        {
            currentNewspaper = Instantiate(NewspaperPrefab, parent);
            currentNewspaper.GetComponent<Image>().sprite = images[i];
            i++;
        }
    }
}