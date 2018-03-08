using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bladetrail2 : MonoBehaviour
{

    public static bladetrail m_bladetrail;

    void Start()
    {
        m_bladetrail = bladetrail.Get();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bladetrail.isAttacking)
        {
            this.gameObject.GetComponent<TrailRenderer>().enabled = true;
        }
        else
        {
            this.gameObject.GetComponent<TrailRenderer>().enabled = false;
        }
    }
}
