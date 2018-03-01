using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScript : MonoBehaviour {

	public Camera cam;
	public GameObject canvas;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space")) {
			cam.GetComponent<CameraFollow> ().enabled = true;
			canvas.SetActive (false);
		}

	}
}
