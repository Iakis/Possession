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

    // Use this for initialization
    void Start () {
        //Debug.Log("switch is not triggered");
        isTriggered = false;
        originPosition = bridge.transform.position;
        isUp = false;
        isDown = true;
	}
	
	// Update is called once per frame
	void Update () {
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
                    UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
                    isUp = true;
                    isDown = false;
                }
            }
        }
        else
        {   
            if (bridge.transform.position.y > originPosition.y)
            {
                bridge.transform.Translate(Vector3.down * Time.deltaTime);
            } else
            {
                if (!isDown)
                {
                    UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
                    isDown = true;
                    isUp = false;
                }
            }
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Izanami" || col.gameObject.name == "Izanagi")
        {
            Debug.Log(string.Format("being stepped by {0}", col.gameObject.name));
            isTriggered = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Izanami" || col.gameObject.name == "Izanagi")
        {
            Debug.Log(string.Format("{0} just left", col.gameObject.name));
            isTriggered = false;
        }
    }
}
