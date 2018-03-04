using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScript : MonoBehaviour {

	public Camera cam;
	public GameObject canvas;
    static Izanagi m_izanagi;
    static Izanami m_izanami;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
        m_izanagi = Izanagi.Get();
        m_izanami = Izanami.Get();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space")) {
			cam.GetComponent<CameraScript>().enabled = true;
			canvas.SetActive (false);
            m_izanagi.enabled = true;
            m_izanami.enabled = true;
		}

	}
}
