using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SwitchScript : MonoBehaviour {

    public Transform bridge;
    public Transform otherSwitch;
    public bool isTriggered;
    public bool otherTriggered;
    public Vector3 originPosition;
    public bool isUp;
    public bool isDown;
    public Transform River;
    public Transform Izanami;
    public Transform Izanagi;
    private float rangeMin, rangeMax;
	private Rigidbody rb;

    // Use this for initialization
    void Start () {
        //Debug.Log("switch is not triggered");
		rb = GetComponent<Rigidbody>();
        isTriggered = false;
        originPosition = bridge.transform.position;
        isUp = false;
        isDown = true;
        //Debug.Log(string.Format("({0}, {1}, {2})", River.transform.position.x, River.transform.position.y, River.transform.position.z));
        RiverGenerator r = River.GetComponent<RiverGenerator>();
        rangeMin = River.transform.position.x - r.gridSize;
        rangeMax = River.transform.position.x + r.gridSize;
        //UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
    }
	
	// Update is called once per frame
	void Update () {
		rb.WakeUp();
        otherTriggered = otherSwitch.GetComponent<SwitchScript>().isTriggered;
		if (isTriggered || otherTriggered)
        {
            if (bridge.transform.position.y < -0.5)
            {
                bridge.transform.Translate(Vector3.up * Time.deltaTime);
            } else
            {
                if (!isUp)
                {
                    //UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
                    isUp = true;
                    isDown = false;
                }
            }
        }
        else
        {
            if (rangeMin < Izanami.position.x  && Izanami.position.x < rangeMax)
            {
                Izanami.transform.position += new Vector3(-(Izanami.position.x - rangeMin + 10), 0f, 0f);
            }
            if (rangeMin < Izanagi.position.x && Izanagi.position.x < rangeMax)
            {
                Izanagi.transform.position += new Vector3(-(Izanagi.position.x - rangeMin + 10), 0f, 0f);
            }
            if (bridge.transform.position.y > originPosition.y)
            {
                bridge.transform.Translate(Vector3.down * Time.deltaTime);

            } else
            {
                if (!isDown)
                {
                    //UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
                    isDown = true;
                    isUp = false;
                }
            }
        }
	}

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "Izanami")
        {
            Debug.Log(string.Format("being stepped by {0}", col.gameObject.name));
            isTriggered = true;
        }
        if (col.gameObject.name == "Izanagi")
        {
            Debug.Log(string.Format("being stepped by {0}", col.gameObject.name));
            isTriggered = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Izanami")
        {
            Debug.Log(string.Format("{0} just left", col.gameObject.name));
            isTriggered = false;
        }
        if (col.gameObject.name == "Izanagi")
        {
            Debug.Log(string.Format("{0} just left", col.gameObject.name));
            isTriggered = false;
        }
    }
}
