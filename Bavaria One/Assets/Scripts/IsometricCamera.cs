using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCamera : MonoBehaviour {

	float minSize = 0.5f, maxSize = 5.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Mouse ScrollWheel") != 0f ) // forward
		{
			float newSize = GetComponent<Camera>().orthographicSize - Input.GetAxis("Mouse ScrollWheel");
			newSize = Mathf.Clamp(newSize, minSize, maxSize);
			GetComponent<Camera>().orthographicSize = newSize;
		}
	}
}
