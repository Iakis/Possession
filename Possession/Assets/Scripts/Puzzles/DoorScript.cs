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
    static Izanagi s_izanagi;
    static Izanami s_izanami;
    public GameObject[] Izanamis;
    public GameObject[] Izanagis;

    // Use this for initialization
    void Start()
    {
        s_izanami = Izanami.Get();
        s_izanagi = Izanagi.Get();
        if (gameObject.name == "Door1")
        {
            isOpenable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (s_izanami == null) s_izanami = Izanami.Get();
        if (s_izanagi == null) s_izanagi = Izanagi.Get();
        if (isOpenable)
        {
            isPlayerIn = (s_izanami.transform.position.x - transform.position.x > 0 && s_izanami.transform.position.x - transform.position.x < 5);
            if (Input.GetKeyDown("space") || Input.GetButtonUp("NamiInteract"))
            {
                if (!isLock && !isOpen && isPlayerIn)
                {
                    Debug.Log("Opening the door when it's not locked and it's closed");
                    transform.position += new Vector3(0f, 0f, 3f);
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
        if (col.gameObject.tag == "Izanami")
        {
            s_izanami.transform.position += Vector3.right;
        }
        if (col.gameObject.tag == "Izanagi")
        {
            s_izanagi.transform.position += Vector3.left;
        }
    }
}