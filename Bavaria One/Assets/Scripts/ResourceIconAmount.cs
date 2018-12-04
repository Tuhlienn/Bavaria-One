using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceIconAmount: MonoBehaviour {
   
   private float lastAmount = 0;
    public float CurrentAmount;

	private TextMeshPro textMesh;
	// Use this for initialization
	void Start () {
		textMesh = gameObject.GetComponent<TextMeshPro>();
		CurrentAmount = 1;
	}
	
	// Update is called once per frame
	void Update () {

		if(CurrentAmount != lastAmount) 
		{
        	textMesh.text = CurrentAmount.ToString("0.00");
			lastAmount = CurrentAmount;
		}
    }
}
