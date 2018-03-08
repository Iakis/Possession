using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour {

    public static Izanagi s_izanagi;
    public static Izanami s_izanami;

    // Use this for initialization
    void Start () {
        s_izanami = Izanami.Get();
        s_izanagi = Izanagi.Get();
    }
	
	// Update is called once per frame
	void Update () {
        if (s_izanami == null || s_izanami.gameObject.layer != 8)
        {
            s_izanami = Izanami.Get();
        }
        if (s_izanagi == null) s_izanagi = Izanagi.Get();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 8)
        {
            StartCoroutine(s_izanami.GetComponent<Izanami>().Respawn());
        }
        if (col.gameObject.layer == 10)
        {
            StartCoroutine(s_izanagi.GetComponent<Izanagi>().Respawn());
        }
    }
}
