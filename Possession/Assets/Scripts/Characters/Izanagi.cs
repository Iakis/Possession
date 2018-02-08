using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Izanagi : MonoBehaviour {

    public float speed = 10;
    static Izanagi s_izanagi;
    static Izanami s_izanami;

    public bool follow;

    public static Izanagi Get()
    {
        return s_izanagi;
    }

    Izanagi()
    {
        s_izanagi = this;
    }
    // Use this for initialization
    void Start () {
        s_izanami = Izanami.Get();
        follow = false;
    }
	
	// Update is called once per frame
	void Update () {

		Vector3 pos = Camera.main.WorldToViewportPoint (transform.position);
		pos.x = Mathf.Clamp01(pos.x);
		pos.y = Mathf.Clamp01(pos.y);
		transform.position = Camera.main.ViewportToWorldPoint(pos);

        if (!s_izanami.follow)
        {
            var x = Input.GetAxis("NagiX") * Time.deltaTime * speed;
            transform.Translate(x, 0, 0);
        } else
        {
            if (s_izanami.form == "ghost")
            {
                var x = Input.GetAxis("NamiX") * Time.deltaTime * speed;
                transform.Translate(x, 0, 0);
            }
        }
    }
}
