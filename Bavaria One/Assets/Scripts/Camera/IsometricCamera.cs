using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCamera : MonoBehaviour {

	public float minSize = 0.5f, maxSize = 5.0f;
	
    void Update () {
		if (Input.GetAxis("Mouse ScrollWheel") != 0f ) // forward
		{
			float newSize = GetComponent<Camera>().orthographicSize - (1.5f * Input.GetAxis("Mouse ScrollWheel"));
			newSize = Mathf.Clamp(newSize, minSize, maxSize);
			GetComponent<Camera>().orthographicSize = newSize;
		}
	}
}
