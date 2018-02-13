using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IzanagiScript : MonoBehaviour
{

    public float speed = 10;
    float y;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("NagiKeyboardX") * Time.deltaTime * speed;
        transform.Translate(x, 0, 0);
        var z = Input.GetAxis("NagiKeyboardY") * Time.deltaTime * speed * 0.5f;
        transform.Translate(0, 0, z);
    }

}
