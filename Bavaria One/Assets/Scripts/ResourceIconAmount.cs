using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceIconAmount: MonoBehaviour {
   
   private float lastAmount = -1;
    public float CurrentAmount;

	private TextMeshPro textMesh;
	// Use this for initialization
	void Start () {
		textMesh = gameObject.GetComponent<TextMeshPro>();
		CurrentAmount = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if(CurrentAmount != lastAmount) 
		{
        	textMesh.text = CurrentAmount == 0.0f ? "" : CurrentAmount.ToString("0.00");
			lastAmount = CurrentAmount;
		}
    }
}
