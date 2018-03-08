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
<<<<<<< HEAD
    static Izanagi s_izanagi;
    static Izanami s_izanami;
=======
    public static Izanagi s_izanagi;
    public static Izanami s_izanami;
>>>>>>> be941b0c9a02392ec1c7a696e60488f54c64dfc4
    private float rangeMin, rangeMax;
	private Rigidbody rb;

    // Use this for initialization
    void Start () {
        //Debug.Log("switch is not triggered");
        s_izanami = Izanami.Get();
        s_izanagi = Izanagi.Get();
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
        //Debug.Log(bridge.transform.position.y);
<<<<<<< HEAD
        if (s_izanami == null) s_izanami = Izanami.Get();
=======
        if (s_izanami == null || s_izanami.gameObject.layer != 8)
        {
            s_izanami = Izanami.Get();
        }
>>>>>>> be941b0c9a02392ec1c7a696e60488f54c64dfc4
        if (s_izanagi == null) s_izanagi = Izanagi.Get();
        rb.WakeUp();
        otherTriggered = otherSwitch.GetComponent<SwitchScript>().isTriggered;
		if (isTriggered || otherTriggered)
        {
            if (bridge.transform.position.y < -2290.1)
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
            if (rangeMin < s_izanami.transform.position.x  && s_izanami.transform.position.x < rangeMax)
            {
                StartCoroutine(s_izanami.GetComponent<Izanami>().Respawn());
            }
            if (rangeMin < s_izanagi.transform.position.x && s_izanagi.transform.position.x < rangeMax)
            {
                StartCoroutine(s_izanagi.GetComponent<Izanagi>().Respawn());
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
<<<<<<< HEAD
        if (col.gameObject.tag == "Izanami" || col.gameObject.tag == "Izanagi")
=======
        if (col.gameObject.layer == 8 || col.gameObject.layer == 10)
>>>>>>> be941b0c9a02392ec1c7a696e60488f54c64dfc4
        {
            Debug.Log(string.Format("being stepped by {0}", col.gameObject.name));
            isTriggered = true;
            //transform.Translate(Vector3.down * 0.1f);
        }
    }

    private void OnTriggerExit(Collider col)
    {
<<<<<<< HEAD
        if (col.gameObject.tag == "Izanami" || col.gameObject.tag == "Izanagi")
=======
        if (col.gameObject.layer == 8 || col.gameObject.layer == 10)
>>>>>>> be941b0c9a02392ec1c7a696e60488f54c64dfc4
        {
            Debug.Log(string.Format("{0} just left", col.gameObject.name));
            isTriggered = false;
            //transform.Translate(Vector3.up * 0.01f);
        }
    }
}
