using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    private GameObject cam;

	// Use this for initialization
	void Start () {

        cam = GameObject.Find("Camera");
	}
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(cam.transform);
	}
}
