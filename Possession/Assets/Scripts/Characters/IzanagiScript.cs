using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IzanagiScript : MonoBehaviour
{

    public float speed = 10;
    private Rigidbody rb;
    public float jumpPower = 100;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var z = Input.GetAxis("NagiKeyboardY") * Time.deltaTime * speed * 0.5f;
        transform.Translate(0, 0, z);
    }

}
