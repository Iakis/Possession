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
    public Transform player;
    Rigidbody rb;

    static DoorScript s_door;
    static Izanami m_izanami;

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (gameObject.name == "Door1")
        {
            isOpenable = true;
        }
        //m_izanami = Izanami.Get();
        //player = m_izanami.transform;
        //Debug.Log(m_izanami.gameObject.name);
        
    }

    DoorScript()
    {
        s_door = this;
        
        
    }

    public static DoorScript Get()
    {
        return s_door;
    }
    // Update is called once per frame
    void Update()
    {
        if (isOpenable)
		{	if (player) {
				isPlayerIn = (player.transform.position.x - transform.position.x > 0);

			}
            if (Input.GetKeyDown("space"))
            {
                if (!isLock && !isOpen)
                {
                    Debug.Log("Opening the door when it's not locked and it's closed");
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f, 2f, 1.5f), Time.deltaTime * 1000f);
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
                    Lock.position = Vector3.MoveTowards(Lock.position, new Vector3(0.45f, 1.5f, -1.43f), 1000f * Time.deltaTime);
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
    }
}