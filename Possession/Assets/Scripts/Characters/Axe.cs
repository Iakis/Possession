using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    
    public bool isAttacking;

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
            //isAttacking = true;
        }
        else
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            //isAttacking = false;
        }
    }
}
