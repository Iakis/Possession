using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public Transform Lock;
    public bool isLock;
    public bool isOpen = false;
    public bool isPlayerIn = false;
    public bool isOpenable = false;
    public Transform Izanami;
    public Transform Izanagi;
    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (gameObject.name == "Door1")
        {
            isOpenable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpenable)
        {
            isPlayerIn = (Izanami.transform.position.x - transform.position.x > 0 && Izanami.transform.position.x - transform.position.x < 5);
            if (Input.GetKeyDown("space") || Input.GetButtonUp("NamiInteract"))
            {
                if (!isLock && !isOpen && isPlayerIn)
                {
                    Debug.Log("Opening the door when it's not locked and it's closed");
                    transform.position += new Vector3(0f, 0f, 1.5f);
                    isOpen = true;
                }
                //if (isOpen)
                //{
                //    Debug.Log("Closing the door when it's opened");
                //    transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f, 2f, 0f), Time.deltaTime * 1000f);
                //    isOpen = false;
                //}
                if (isPlayerIn && isLock)
                {
                    Debug.Log("Unlock the door when it's locked and player is inside the door");
                    Lock.position += new Vector3(0.45f, 0f, -1.43f);
                    isLock = false;
                }
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (isOpenable)
        {
            if (col.gameObject.name == "Lock")
            {
                isLock = true;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Oni")
        {
            Destroy(this.gameObject);
        }
        if (col.gameObject.name == "Izanami")
        {
            Izanami.transform.position += Vector3.right;
        }
        if (col.gameObject.name == "Izanagi")
        {
            Izanagi.transform.position += Vector3.left;
        }
    }
}