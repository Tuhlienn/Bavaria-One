using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {

	private Vector3 deltaPosition;
	public float CameraSpeed;
	public float SmoothingFactor = 0.8f;

	public float PanningThreshold = 50f;
	public bool EnablePanning = true; 

	private Rect bounds;

	// Use this for initialization
	void Start () {
		bounds = new Rect(-10, 0, 25, 15);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Horizontal") != 0f )
		{
			deltaPosition.x += Input.GetAxis("Horizontal") * CameraSpeed * Time.deltaTime;
		}
		if (Input.GetAxis("Vertical") != 0f )
		{
			deltaPosition.y += Input.GetAxis("Vertical") * CameraSpeed * Time.deltaTime;
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
			if (Input.mousePosition.y >= Screen.height - PanningThreshold) {
				// Move the camera
				deltaPosition.y += CameraSpeed * Time.deltaTime;
			}
			if (Input.mousePosition.y <= PanningThreshold) {
				// Move the camera
				deltaPosition.y -= CameraSpeed * Time.deltaTime;
			}
		}

		Vector3 toMove = Vector3.Lerp(Vector3.zero, deltaPosition, SmoothingFactor);
		deltaPosition = deltaPosition - toMove;
		if(deltaPosition.magnitude < 0.05f) {
			deltaPosition = Vector3.zero;
		}
		transform.Translate(toMove);

		// Vector3 newPos = new Vector3();
		// newPos.x = Mathf.Clamp(transform.position.x, bounds.xMin, bounds.xMax);
		// newPos.y = Mathf.Clamp(transform.position.y, bounds.yMin, bounds.yMax);

		// transform.position = newPos;
	}
}
