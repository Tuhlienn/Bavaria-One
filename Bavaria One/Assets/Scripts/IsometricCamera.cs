using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCamera : MonoBehaviour {

	public float minSize = 0.5f, maxSize = 5.0f;
    public AudioClip main;

	// Use this for initialization
	void Start () {
        SoundManager.Instance.PlayMusic(main);
    }

    // Use this for initialization
    void Awake()
    {
       
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetAxis("Mouse ScrollWheel") != 0f ) // forward
		{
			float newSize = GetComponent<Camera>().orthographicSize - (1.5f * Input.GetAxis("Mouse ScrollWheel"));
			newSize = Mathf.Clamp(newSize, minSize, maxSize);
			GetComponent<Camera>().orthographicSize = newSize;
		}
	}
}
