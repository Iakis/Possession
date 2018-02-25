using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bladetrail : MonoBehaviour {

    public static bladetrail s_bladetrail;

    public static bladetrail Get()
    {
        return s_bladetrail;
    }

    public bool isAttacking;

    bladetrail()
    {
        s_bladetrail = this;
    }
    void Start () {
        isAttacking = false;

    }
	
	// Update is called once per frame
	void Update () {
		if (isAttacking)
        {
            this.gameObject.GetComponent<TrailRenderer>().enabled = true;
        } else
        {
            this.gameObject.GetComponent<TrailRenderer>().enabled = false;
        }
	}
}
