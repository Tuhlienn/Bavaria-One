using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {

	private Vector3 deltaPosition;
	public float CameraSpeed;
	public float SmoothingFactor = 0.8f;

	public float PanningThreshold = 50f;
	public bool EnablePanning = true; 

	public float xMin = -45.0f, xMax = 45.0f, zMin = -45.0f, zMax = 45.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Horizontal") != 0f )
		{
			deltaPosition.x += Input.GetAxis("Horizontal") * CameraSpeed * Time.deltaTime;
		}
		if (Input.GetAxis("Vertical") != 0f )
		{
			deltaPosition.z += Input.GetAxis("Vertical") * CameraSpeed * Time.deltaTime;
		}

		if(EnablePanning) 
		{
			if (Input.mousePosition.x >= Screen.width - PanningThreshold) {
				// Move the camera
				deltaPosition.x += CameraSpeed * Time.deltaTime;
			}
			if (Input.mousePosition.x <= PanningThreshold) {
				// Move the camera
				deltaPosition.x -= CameraSpeed * Time.deltaTime;
			}
			if (Input.mousePosition.z >= Screen.height - PanningThreshold) {
				// Move the camera
				deltaPosition.z += CameraSpeed * Time.deltaTime;
			}
			if (Input.mousePosition.z <= PanningThreshold) {
				// Move the camera
				deltaPosition.z-= CameraSpeed * Time.deltaTime;
			}
		}

		Vector3 toMove = Vector3.Lerp(Vector3.zero, deltaPosition, SmoothingFactor);
		deltaPosition = deltaPosition - toMove;
		if(deltaPosition.magnitude < 0.05f) {
			deltaPosition = Vector3.zero;
		}
		toMove = Quaternion.Euler(0, 45, 0) * toMove;
		transform.Translate(toMove, Space.World);

		Vector3 newPos = new Vector3();
		newPos.x = Mathf.Clamp(transform.position.x, xMin, xMax);
		newPos.z = Mathf.Clamp(transform.position.z, zMin, zMax);

		transform.position = newPos;
	}
}
