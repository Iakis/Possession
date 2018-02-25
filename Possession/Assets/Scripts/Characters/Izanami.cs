﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Izanami : MonoBehaviour
{

    public float speed = 10;
    public string form;
    public bool following;
    public float gravityScale = 1.0f;
    public float height;
    Quaternion rot;
    Quaternion rot2;

    bool CD;
    bool press;
    bool grounded;
    Animator anim;
    float jumpheight;
    static Izanagi m_izanagi;
    static Izanami s_izanami;
    Rigidbody m_Rigidbody;

    static float globalGravity = -9.81f;

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
        anim = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        grounded = true;
        rot = transform.rotation;
        rot2 = Quaternion.Inverse(rot);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        Debug.Log(pos.x);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
        m_Rigidbody.AddForce(gravity, ForceMode.Acceleration);

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

    void move()
    {
        if (following)
        {
            var x = Input.GetAxis("NamiX");
            var y = Input.GetAxis("NamiY");
            walk(x, y);
            jump(y);

        }
        else if (s_izanami.form == "ghost")
        {
            var x = Input.GetAxis("NamiX");
            var y = Input.GetAxis("NamiY");
            walk(x, y);
            jump(y);

        }
    }

    void jump(float y)
    {
        if (y < 0)
        {
            anim.SetBool("idle", false);
            if (grounded)
            {
                m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, height, 0);
                grounded = false;
                if (m_Rigidbody.velocity.x == 0)
                {
                    anim.SetBool("standjump", true);
                }
                else
                {
                    anim.SetBool("jumping", true);
                }
            }
            else
            {
                //anim.SetBool("inair", true);
                m_Rigidbody.velocity = m_Rigidbody.velocity;
            }
        }
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
        anim.SetBool("casting", true);
        Debug.Log("shielded");
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("casting", false);
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

    void walk(float x, float y)
    {
        //anim.SetBool("idle", false);
        anim.SetBool("walking", true);

        if (x > 0)
        {
            if (transform.rotation == rot)
            {
                m_Rigidbody.velocity = new Vector3(speed, m_Rigidbody.velocity.y, 0);
            }
            else
            {
                //transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * turnspeed);
                transform.rotation = rot;
                m_Rigidbody.velocity = new Vector3(speed, m_Rigidbody.velocity.y, 0);
            }

        }
        else if (x < 0)
        {
            if (transform.rotation == rot2)
            {
                m_Rigidbody.velocity = new Vector3(-speed, m_Rigidbody.velocity.y, 0);
            }
            else
            {
                //transform.rotation = Quaternion.Lerp(transform.rotation, rot2, Time.deltaTime * turnspeed);
                transform.rotation = rot2;
                m_Rigidbody.velocity = new Vector3(-speed, m_Rigidbody.velocity.y, 0);
            }

        }
        else
        {
            anim.SetBool("walking", false);
            m_Rigidbody.velocity = new Vector3(0, m_Rigidbody.velocity.y, 0);
        }
        jump(y);

    }

    void followMove()
    {
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

    void OnCollisionEnter(Collision collide)
    {
        Debug.Log(collide.gameObject.tag);
        if (collide.gameObject.tag == "ground")
        {
            grounded = true;
            anim.SetBool("jumping", false);
            anim.SetBool("standjump", false);
        }
    }
}
