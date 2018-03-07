using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour {

    static Izanagi s_izanagi;
    static Izanami s_izanami;

    // Use this for initialization
    void Start () {
        s_izanami = Izanami.Get();
        s_izanagi = Izanagi.Get();
    }
	
	// Update is called once per frame
	void Update () {
        if (s_izanami == null) s_izanami = Izanami.Get();
        if (s_izanagi == null) s_izanagi = Izanagi.Get();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Izanami")
        {
            StartCoroutine(s_izanami.GetComponent<Izanagi>().Respawn());
        }
        if (col.gameObject.tag == "Izanagi")
        {
            StartCoroutine(s_izanagi.GetComponent<Izanagi>().Respawn());
        }
    }
}
