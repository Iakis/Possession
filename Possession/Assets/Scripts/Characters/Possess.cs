using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possess : MonoBehaviour {

    static Izanami s_izanami;
    bool press = false;

    GameObject host;

    // Use this for initialization
    void Start () {
        s_izanami = Izanami.Get();
    }
	
	// Update is called once per frame
	void Update () {
        targets(s_izanami.transform.position, 3);
        //possess();
	}

    void targets(Vector3 center, float radius)
    {
        int layerMask = 1 << 10;
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, layerMask);
        int i = 0;
        while (i < hitColliders.Length)
        {
            host = hitColliders[i].gameObject;
            return;
        }
    }

    void possess()
    {
        if (Input.GetAxis("NamiInteract") != 0)
        {
            press = true;
        } else if (Input.GetAxis("NamiInteract") == 0)
        {
            if (press == true)
            {
                Debug.Log("possess");
                press = false;
            }
        }
    }
}
