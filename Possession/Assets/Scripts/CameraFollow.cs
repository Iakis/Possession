using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour {

	public List<Transform> targets;

	public Vector3 offset;
	public float smoothTime = .5f;

	public float minZoom = 200f;
	public float maxZoom = 50;
	public float zoomLimiter = 30f;

	private Vector3 velocity;
	private Camera cam;

	void Start(){
		cam = GetComponent<Camera> ();
	}

	void LateUpdate(){
		Move ();
		Zoom ();
	}

	void Move(){
		Vector3 centerPoint = GetCenterPoint ();

		Vector3 newPosition = centerPoint + offset;

		transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
	}

	void Zoom(){
		//Debug.Log (GetGreatestDistance());
		float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / 45f);
		cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
	}

	float GetGreatestDistance(){
		var bounds = new Bounds (targets [0].position, Vector3.zero);
		for (int i = 0; i < targets.Count; i++) {
			bounds.Encapsulate (targets [i].position);
		}

		return bounds.size.x;
	}

	Vector3 GetCenterPoint(){
		var bounds = new Bounds (targets [0].position, Vector3.zero);
		for (int i = 0; i < targets.Count; i++) {
			bounds.Encapsulate (targets [i].position);
		}

		return bounds.center;
	}
}
