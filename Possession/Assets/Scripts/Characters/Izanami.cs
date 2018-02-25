using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Izanami : MonoBehaviour
{

    public float speed = 10;
    public string form;
    public bool following;

    bool CD;
    bool press;
    float jumpheight;
    static Izanagi m_izanagi;
    static Izanami s_izanami;

    public static Izanami Get()
    {
        return s_izanami;
    }

    public Izanami()
    {
        s_izanami = this;

    }
    // Use this for initialization
    void Start()
    {
        m_izanagi = Izanagi.Get();
        form = "ghost";
        following = false;
        CD = false;
        press = false;
    }

    // Update is called once per frame
    public virtual void Update()
    {

        follow();
        if (following)
        {
            followMove();
        }
        else
        {
            move();
        }
        attack();

    }

    public virtual void attack()
    {
        if (Input.GetAxis("NamiAttack") != 0)
        {
            press = true;
        }
        else if (Input.GetAxis("NamiAttack") == 0)
        {
            if (press == true)
            {
                if (!CD)
                {
                    StartCoroutine("shield");
                    StartCoroutine("cooldown");
                }
                press = false;
            }
        }
    }

    IEnumerator shield()
    {
        // suspend execution for 5 seconds
        m_izanagi.shielded = true;
        Debug.Log("shielded");
        yield return new WaitForSeconds(0.5f);
        m_izanagi.shielded = false;
        Debug.Log("un-shielded");
    }

    IEnumerator cooldown()
    {
        // suspend execution for 5 seconds
        CD = true;
        yield return new WaitForSeconds(3);
        CD = false;
    }

    public virtual void follow()
    {
        if (Input.GetButtonUp("Ybutton"))
        {
            if (following == true)
            {
                following = false;
                m_izanagi.follow = false;
            }
            else if (((m_izanagi.transform.position.x - this.transform.position.x < 4)
                && (m_izanagi.transform.position.x - this.transform.position.x > -4))
                && following == false)
            {
                following = true;
                m_izanagi.follow = true;
                {
                    link();
                }
            }
        }
    }

    public virtual void link()
    {
        Vector3 start = new Vector3(this.transform.transform.position.x, this.transform.position.y, this.transform.position.z);
        Vector3 end = new Vector3();
        if (m_izanagi.transform.position.x > this.transform.position.x)
        {
            end = new Vector3(m_izanagi.transform.transform.position.x - 1.0f, this.transform.position.y, this.transform.position.z);
            this.transform.position = Vector3.Lerp(start, end, speed / 5 * Time.deltaTime);
        }
        else
        {
            end = new Vector3(m_izanagi.transform.transform.position.x + 1.0f, this.transform.position.y, this.transform.position.z);
            this.transform.position = Vector3.Lerp(start, end, speed / 5 * Time.deltaTime);
        }
    }

    void move()
    {

		Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
		Debug.Log (pos.x);
		pos.x = Mathf.Clamp01(pos.x) ;
		pos.y = Mathf.Clamp01(pos.y);
		transform.position = Camera.main.ViewportToWorldPoint(pos);

        if (Input.GetAxis("NamiX") != 0)
        {
            var x = Input.GetAxis("NamiX") * Time.deltaTime * speed;
            transform.Translate(x, 0, 0);
        }
        else
        {
            this.transform.position = this.transform.position;
        }
    }

    void followMove()
    {
		Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
		Debug.Log (pos.x);
		pos.x = Mathf.Clamp01(pos.x) ;
		pos.y = Mathf.Clamp01(pos.y);
		transform.position = Camera.main.ViewportToWorldPoint(pos);

        if (m_izanagi.transform.position.x - this.transform.position.x > 1.0)
        {
            Vector3 start = new Vector3(this.transform.transform.position.x, this.transform.position.y, this.transform.position.z);
            Vector3 end = new Vector3(m_izanagi.transform.transform.position.x - 1.0f, this.transform.position.y, this.transform.position.z);
            this.transform.position = Vector3.Lerp(start, end, speed * Time.deltaTime);
        }
        else if (m_izanagi.transform.position.x - this.transform.position.x < -1.0)
        {
            Vector3 start = new Vector3(this.transform.transform.position.x, this.transform.position.y, this.transform.position.z);
            Vector3 end = new Vector3(m_izanagi.transform.transform.position.x + 1.0f, this.transform.position.y, this.transform.position.z);
            this.transform.position = Vector3.Lerp(start, end, speed * Time.deltaTime);
        }
    }
}
