using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour {

<<<<<<< HEAD
    static Izanagi s_izanagi;
    static Izanami s_izanami;
=======
    public static Izanagi s_izanagi;
    public static Izanami s_izanami;
>>>>>>> be941b0c9a02392ec1c7a696e60488f54c64dfc4

    // Use this for initialization
    void Start () {
        s_izanami = Izanami.Get();
        s_izanagi = Izanagi.Get();
    }
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
        if (s_izanami == null) s_izanami = Izanami.Get();
=======
        if (s_izanami == null || s_izanami.gameObject.layer != 8)
        {
            s_izanami = Izanami.Get();
        }
>>>>>>> be941b0c9a02392ec1c7a696e60488f54c64dfc4
        if (s_izanagi == null) s_izanagi = Izanagi.Get();
    }

    void OnCollisionEnter(Collision col)
    {
<<<<<<< HEAD
        if (col.gameObject.tag == "Izanami")
        {
            StartCoroutine(s_izanami.GetComponent<Izanami>().Respawn());
        }
        if (col.gameObject.tag == "Izanagi")
=======
        if (col.gameObject.layer == 8)
        {
            StartCoroutine(s_izanami.GetComponent<Izanami>().Respawn());
        }
        if (col.gameObject.layer == 10)
>>>>>>> be941b0c9a02392ec1c7a696e60488f54c64dfc4
        {
            StartCoroutine(s_izanagi.GetComponent<Izanagi>().Respawn());
        }
    }
}
