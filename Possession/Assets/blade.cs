using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blade : MonoBehaviour {

    public static blade s_blade;
    bladetrail m_bladetrail;
    public bool isAttacking;

    public static blade Get()
    {
        return s_blade;
    }

    blade()
    {
        s_blade = this;
    }
	// Use this for initialization
	void Start () {
        m_bladetrail = bladetrail.Get();
        isAttacking = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isAttacking)
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
            m_bladetrail.isAttacking = true;
        } else
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            m_bladetrail.isAttacking = false;
        }
	}
}
