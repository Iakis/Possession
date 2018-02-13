using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;
    public float sec = 10f;
    private float timer;
    public Transform Lock;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
        timer = Time.time;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        timer += Time.deltaTime;
        //if (timer > 0 && timer < 2)
        //{
        //    transform.Translate((Vector3.right * (Time.deltaTime * 10.0f)));
        //}
        //else if (timer >= 2 && timer <= sec - 2)
        //{
        //    Lock.position = Vector3.MoveTowards(Lock.position, new Vector3(0.3f, 1.5f, -0.73f), 0.15f * Time.deltaTime);
        //}
        //else if (timer > sec - 2 && timer <= sec)
        //{
        //    transform.Translate((Vector3.left * (Time.deltaTime * 10.0f)));
        //}
        //else if (timer > sec)
        //{
           transform.position = player.transform.position + offset;
        //}
    }
}

