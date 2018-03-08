using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    public bool isActivated;
    private GameObject[] respawns;

	// Use this for initialization
	void Start () {
        isActivated = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {
<<<<<<< HEAD
        if (col.gameObject.tag == "Izanami" || col.gameObject.tag == "Izanagi")
=======
        if (col.gameObject.layer == 8 || col.gameObject.layer == 10)
>>>>>>> be941b0c9a02392ec1c7a696e60488f54c64dfc4
        {
            isActivated = true;
            respawns = GameObject.FindGameObjectsWithTag("Respawn");
            foreach (GameObject respawn in respawns)
            {
                if (respawn.GetComponent<Respawn>().isActivated && respawn.name != this.name)
                {
                    respawn.GetComponent<Respawn>().isActivated = false;
                }
            }
        }
    }
}
