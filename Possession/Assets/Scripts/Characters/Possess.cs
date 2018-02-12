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
        host = null;
    }
	
	// Update is called once per frame
	void Update () {
        targets(s_izanami.transform.position, 3);
        possess();
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
        host = null;
    }

    void possess()
    {
        if (Input.GetButtonUp("NamiInteract"))
        {
            if (host != null)
            {
                //DoorScript m_door = DoorScript.Get();
                //Debug.Log(host);
                //m_door.player = host.transform;
                //Debug.Log(m_door.player);
                //host.GetComponent<Oni1Script>().dead = false;
                host.AddComponent<Izanami>().enabled = true;
                host.GetComponent<Oni1Script>().enabled = false;
                host.GetComponent<Animator>().enabled = false;
                host.transform.rotation = Quaternion.identity;
                Destroy(this.gameObject);

            }
        } 
    }
}
