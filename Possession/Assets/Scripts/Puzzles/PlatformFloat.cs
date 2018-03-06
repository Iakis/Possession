using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFloat : MonoBehaviour {

    public float rate = 5f;
    public float newHeight;
    public float speed = 5f;
    public float curHeight;

	// Use this for initialization
	void Start () {
        curHeight = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.name == "Platform-updown")
        {
            StartCoroutine("Floating");
        } 
        //if (isUp)
        //{
        //    if (gameObject.name == "Platform-updown")
        //    {
        //        StartCoroutine("PlatformUp");
        //    }
        //} else
        //{
        //    if (gameObject.name == "Platform-updown")
        //    {
        //        StartCoroutine("PlatformDown");
        //    }
        //}
	}

    IEnumerator Floating()
    {
        yield return new WaitForSeconds(2f);
        newHeight = Mathf.Cos(Time.time * 1f);
        Vector3 new_pos = new Vector3(0f, newHeight, 0f);
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + new_pos * rate, step);
    }

    void OnTriggerStay(Collider col)
    {
        Debug.Log("Hey");
        if (gameObject.name == "Platform-high")
        {
            if ((col.gameObject.name == "Izanami" || col.gameObject.name == "Izanagi") && transform.position.y > curHeight - 10f)
            {
                transform.Translate(Vector3.down * Time.deltaTime);
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (gameObject.name == "Platform-high")
        {
            if ((col.gameObject.name == "Izanami" || col.gameObject.name == "Izanagi") && transform.position.y < curHeight)
            {
                transform.Translate(Vector3.up * Time.deltaTime);
            }
        }
    }
}
