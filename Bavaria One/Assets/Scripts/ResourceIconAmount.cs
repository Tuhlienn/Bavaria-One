using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceIconAmountScript: MonoBehaviour {
    public float CurrentAmount;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		TextMeshPro mText = gameObject.GetComponent<TextMeshPro>();

        mText.text = CurrentAmount.ToString("0.00");
    }
}
