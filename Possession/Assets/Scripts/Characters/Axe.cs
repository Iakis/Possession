using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{

    public static Axe s_axe;
    public bool isAttacking;

    public static Axe Get()
    {
        return s_axe;
    }

    Axe()
    {
        s_axe = this;
    }
    // Use this for initialization
    void Start()
    {
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
            //m_bladetrail.isAttacking = true;
        }
        else
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            //m_bladetrail.isAttacking = false;
        }
    }
}
