﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Izanagi : MonoBehaviour
{

    public float speed = 10;
    public float gravityScale = 1.0f;
    static Izanagi s_izanagi;
    static Izanami s_izanami;
    static blade m_blade;
    Rigidbody m_Rigidbody;
    Animator anim;
    private Quaternion rot;
    private Quaternion rot2;
    private GameObject[] respawns;

    public bool follow;
    public bool shielded;
    public float height;
    public float turnspeed;

    AudioSource swordSound;
    AudioSource jumpSound;
    AudioSource walkSound;

    bool CD;
    bool press;
    bool isjumping;
    bool grounded;
    bool idle;
    bool comb;
    int combo;
    bool dead;

    static float globalGravity = -9.81f;

    public static Izanagi Get()
    {
        return s_izanagi;
    }

    Izanagi()
    {
        s_izanagi = this;
    }
    // Use this for initialization
    void Start()
    {
        s_izanami = Izanami.Get();
        m_blade = blade.Get();
        anim = GetComponent<Animator>();
        follow = false;
        m_Rigidbody = GetComponent<Rigidbody>();
        rot = transform.rotation;
        rot2 = Quaternion.Inverse(rot);
        CD = false;
        press = false;
        shielded = false;
        grounded = true;
        swordSound = GameObject.Find("SwordSound").GetComponent<AudioSource>();
        jumpSound = GameObject.Find("JumpSound").GetComponent<AudioSource>();
        walkSound = GameObject.Find("WalkSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        walkSound.mute = !(anim.GetBool("isWalking"));
        if (s_izanami == null) s_izanami = Izanami.Get();
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        m_Rigidbody.AddForce(gravity, ForceMode.Acceleration);
        if (!CD && !dead)
        {
            move();
        }
        if (!dead)
        {
            attack();
        }
    }

    public void attack()
    {
        if (Input.GetAxis("NagiAttack") != 0)
        {
            press = true;
        }
        else if (Input.GetAxis("NagiAttack") == 0)
        {
            if (press == true)
            {
                if (!CD && combo == 0)
                {
                    StartCoroutine("cooldown");
                    anim.SetTrigger("attacking");
                    StartCoroutine("playSword");
                    combo = 1;
                } else if (comb && combo == 1)
                {
                    StopCoroutine("cooldown");
                    StartCoroutine("cooldown");
                    anim.SetTrigger("attacking2");
                    StartCoroutine("playSword");
                    combo = 2;
                } else if (comb && combo == 2)
                {
                    StopCoroutine("cooldown");
                    StartCoroutine("cooldown");
                    anim.SetTrigger("attacking3");
                    StartCoroutine("playSword");
                    combo = 3;
                }
                press = false;
            }
        }
    }

    IEnumerator playSword()
    {
        yield return new WaitForSeconds(0.65f);
        swordSound.Play();
    }

    IEnumerator cooldown()
    {
        CD = true;
        comb = false;
        anim.SetBool("idle", false);
        m_blade.isAttacking = true;
        yield return new WaitForSeconds(0.25f);
        comb = true;
        if (combo == 1)
        {
            yield return new WaitForSeconds(0.3f);
        } else if (combo == 2)
        {
            yield return new WaitForSeconds(0.55f);
        } else if (combo == 3)
        {
            yield return new WaitForSeconds(0.85f);
        }
        anim.SetBool("idle", true);
        combo = 0;
        CD = false;
        m_blade.isAttacking = false;
        
        comb = false;
    }

    //IEnumerator turn()
    //{
    //    // suspend execution for 5 seconds
    //    anim.SetBool("turning", true);
    //    anim.SetBool("isWalking", false);
    //    
    //    yield return new WaitForSeconds(2.09f);
    //
    //
    //    anim.SetBool("turning", false);
    //    if (transform.rotation == rot2)
    //    {
    //        //transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * turnspeed);
    //        transform.rotation = rot;
    //    }
    //    else if (transform.rotation == rot)
    //    {
    //        //transform.rotation = Quaternion.Lerp(transform.rotation, rot2, Time.deltaTime * turnspeed);
    //        transform.rotation = rot2;
    //    }
    //    
    //}

    void move()
    {
        if (!s_izanami.following)
        {
            var x = Input.GetAxis("NagiX");
            var y = Input.GetAxis("NagiY");
            walk(x, y);
            jump(y);
        }
        else if (s_izanami.form == "ghost")
        {
            var x = Input.GetAxis("NamiX") * Time.deltaTime * speed;
            var y = Input.GetAxis("NamiY");
            walk(x, y);

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
                anim.SetTrigger("jump");
                jumpSound.Play();
            }
            else
            {
                m_Rigidbody.velocity = m_Rigidbody.velocity;
            }
        }
    }

    void walk(float x, float y)
    {
        // Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        // pos.x = Mathf.Clamp01(pos.x);
        // pos.y = Mathf.Clamp01(pos.y);
        // transform.position = Camera.main.ViewportToWorldPoint(pos);
        anim.SetBool("idle", false);
        anim.SetBool("isWalking", true);

        if (x > 0 && CD == false)
        {
            if (transform.rotation == rot)
            {
                m_Rigidbody.velocity = new Vector3(speed, m_Rigidbody.velocity.y, 0);
            } else
            {
                //transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * turnspeed);
                transform.rotation = rot;
                m_Rigidbody.velocity = new Vector3(speed, m_Rigidbody.velocity.y, 0);
            }
            
        }
        else if (x < 0 && CD == false)
        {
            if (transform.rotation == rot2)
            {
                m_Rigidbody.velocity = new Vector3(-speed, m_Rigidbody.velocity.y, 0);
            } else
            {
                //transform.rotation = Quaternion.Lerp(transform.rotation, rot2, Time.deltaTime * turnspeed);
                transform.rotation = rot2;
                m_Rigidbody.velocity = new Vector3(-speed, m_Rigidbody.velocity.y, 0);
            }

        }
        else
        {
            anim.SetBool("isWalking", false);
            m_Rigidbody.velocity = new Vector3(0, m_Rigidbody.velocity.y, 0);
            anim.SetBool("idle", true);
        }
        jump(y);
        
    }

    void OnCollisionEnter(Collision collide)
    {
        //Debug.Log(collide.gameObject.tag);
        if (collide.gameObject.tag == "ground")
        {
            grounded = true;            
        }
    }

    IEnumerator Die()
    {
        dead = true;
        
        Debug.Log(string.Format("{0} is dead", gameObject.name));
        yield return new WaitForSeconds(3f);
        StartCoroutine("Respawn");
        //animation for death
    }

    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1f);
        dead = false;
        anim.SetTrigger("revive");
        respawns = GameObject.FindGameObjectsWithTag("Respawn");
        foreach (GameObject respawn in respawns)
        {
            if (respawn.GetComponent<Respawn>().isActivated)
            {
                transform.position = respawn.transform.position;
                s_izanami.transform.position = respawn.transform.position + Vector3.left*5f;
            }
        }
        //animation for respawn
    }

    void OnTriggerEnter(Collider col)
    {
        //if (col.gameObject.name == "Axe")
        //{
        //    Debug.Log(string.Format("{0} gets hit", gameObject.name));
        //    anim.SetTrigger("die");
        //    StartCoroutine("Die");
        //}
    }
}
