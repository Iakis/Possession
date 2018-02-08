using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Izanami : MonoBehaviour
{

    public float speed = 10;
    static Izanagi s_izanagi;
    static Izanami s_izanami;
    public string form;
    public bool follow;

    public static Izanami Get()
    {
        return s_izanami;
    }

    Izanami()
    {
        s_izanami = this;
        
    }
    // Use this for initialization
    void Start()
    {
        s_izanagi = Izanagi.Get();
        form = "ghost";
        follow = false;
    }

    // Update is called once per frame
    void Update()
    {

		Vector3 pos = Camera.main.WorldToViewportPoint (transform.position);
		pos.x = Mathf.Clamp01(pos.x);
		pos.y = Mathf.Clamp01(pos.y);
		transform.position = Camera.main.ViewportToWorldPoint(pos);

        if (Input.GetButtonUp("Ybutton"))
        {
            Debug.Log("y");
            if (follow == true)
            {
                Debug.Log("unfollow");
                follow = false;
            }
            else if (((s_izanagi.transform.position.x - this.transform.position.x < 4)
                && (s_izanagi.transform.position.x - this.transform.position.x > -4))
                && follow == false)
            {
                Debug.Log("follow");
                follow = true;
                if (this.form == "ghost")
                {
                    Vector3 start = new Vector3(this.transform.transform.position.x, this.transform.position.y, this.transform.position.z);
                    Vector3 end = new Vector3();
                    if (s_izanagi.transform.position.x > this.transform.position.x)
                    {
                        end = new Vector3(s_izanagi.transform.transform.position.x - 1.0f, this.transform.position.y, this.transform.position.z);
                        s_izanami.transform.position = Vector3.Lerp(start, end, speed/2 * Time.deltaTime);
                    } else
                    {
                        end = new Vector3(s_izanagi.transform.transform.position.x + 1.0f, this.transform.position.y, this.transform.position.z);
                        s_izanami.transform.position = Vector3.Lerp(start, end, speed/2 * Time.deltaTime);
                    }
                }
            }
        }

        if (!follow)
        {
            if (Input.GetAxis("NamiX") != 0)
            {
                var x = Input.GetAxis("NamiX") * Time.deltaTime * speed;
                transform.Translate(x, 0, 0);
            }
            else
            {
                this.transform.position = this.transform.position;
            }
        } else
        {
            if (form == "ghost")
            {
                if (s_izanagi.transform.position.x - this.transform.position.x > 1.0)
                {
                    Vector3 start = new Vector3(this.transform.transform.position.x, this.transform.position.y, this.transform.position.z);
                    Vector3 end = new Vector3(s_izanagi.transform.transform.position.x - 1.0f, this.transform.position.y, this.transform.position.z);
                    this.transform.position = Vector3.Lerp(start, end, speed * Time.deltaTime);
                } else if (s_izanagi.transform.position.x - this.transform.position.x < -1.0)
                {
                    Vector3 start = new Vector3(this.transform.transform.position.x, this.transform.position.y, this.transform.position.z);
                    Vector3 end = new Vector3(s_izanagi.transform.transform.position.x + 1.0f, this.transform.position.y, this.transform.position.z);
                    this.transform.position = Vector3.Lerp(start, end, speed * Time.deltaTime);
                }
            }
        }
        
        
         
        
    }
}
