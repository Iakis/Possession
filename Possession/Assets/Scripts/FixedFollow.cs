using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFollow : MonoBehaviour {

	private Camera cam;
	public Transform Izanami;
	public Transform Izanagi;
	public Vector3 offset;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		FixedCameraFollowSmooth (cam, Izanami, Izanagi);
	}

	// Follow Two Transforms with a Fixed-Orientation Camera
	public void FixedCameraFollowSmooth(Camera cam, Transform t1, Transform t2)
	{
		// How many units should we keep from the players
		float zoomFactor = 1f;
		float followTimeDelta = 0.8f;

		// Midpoint we're after
		Vector3 midpoint = (t1.position + t2.position) / 2f;

		// Distance between objects
		float distance = (t1.position - t2.position).magnitude;

		// Move camera a certain distance
		Vector3 cameraDestination = midpoint - cam.transform.forward * distance * zoomFactor + offset;

		// Adjust ortho size if we're using one of those
		if (cam.orthographic)
		{
			// The camera's forward vector is irrelevant, only this size will matter
			cam.orthographicSize = distance;
		}
		// You specified to use MoveTowards instead of Slerp
		cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta);

		// Snap when close enough to prevent annoying slerp behavior
		if ((cameraDestination - cam.transform.position).magnitude <= 0.05f)
			cam.transform.position = cameraDestination;
	}
}
