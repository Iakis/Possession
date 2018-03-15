using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possess : MonoBehaviour {

    static Izanami s_izanami;
    bool press = false;
    CameraScript cam;

    GameObject host;

    // Use this for initialization
    void Start () {
        s_izanami = Izanami.Get();
        host = null;
        cam = CameraScript.get();
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
            if (hitColliders[i].gameObject.name != "sword")
            {
                host = hitColliders[i].gameObject;
                
            }
            return;

        }
        host = null;
    }

    void possess()
    {
        if (Input.GetButtonUp("NamiInteract"))
        {
			// Check if host tag is oni
			if (host != null && host.CompareTag("Oni") && host.GetComponent<OniAI>().hasBeenPossessed == false)
            {
				Debug.Log ("Hosts: ");
				Debug.Log (host);
				Debug.Log ("Possessing Oni");
                //DoorScript m_door = DoorScript.Get();
                //Debug.Log(host);
                //m_door.player = host.transform;
                //Debug.Log(m_door.player);
                //host.GetComponent<Oni1Script>().dead = false;
                //cam.player = host;
                host.AddComponent<IzaOni>().enabled = true;
                //host.GetComponent<IzaOni>().Start();
				host.GetComponent<OniAI>().hasBeenPossessed = true;
                host.GetComponent<OniAI>().enabled = false;
                //host.transform.rotation = this.transform.rotation;
                host.GetComponent<Animator>().SetBool("die", false);
                host.GetComponent<Animator>().SetBool("revive", true);

				// Get rid of halo
				host.GetComponent ("Halo").GetType ().GetProperty ("enabled").SetValue (host.GetComponent ("Halo"), false, null);
                //Destroy(this.gameObject);
				host.GetComponent<IzaOni>().izanami = this.gameObject;
				this.gameObject.SetActive(false);
            }
        } 
    }
}
