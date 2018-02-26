using System.Collections;
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
    Quaternion rot;
    Quaternion rot2;

    public bool follow;
    public bool shielded;
    public float height;
    public float turnspeed;

    bool CD;
    bool press;
    bool isjumping;
    bool grounded;
    bool idle;

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
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        m_Rigidbody.AddForce(gravity, ForceMode.Acceleration);
        if (!CD)
        {
            move();
        }
        attack();
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

                if (!CD)
                {
                    StartCoroutine("slice");
                    StartCoroutine("cooldown");
                }
                press = false;
            }
        }
        //----------KEYBOARD-------------
        if (Input.GetKey("down"))
        {
            if (!CD)
            {
                StartCoroutine("slice");
                StartCoroutine("cooldown");
            }
        }
        //----------KEYBOARD-------------

    }

    IEnumerator slice()
    {
        // suspend execution for 5 seconds
        anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.3f);
        m_blade.isAttacking = true;
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("isAttacking", false);
        m_blade.isAttacking = false;
    }

    IEnumerator cooldown()
    {
        // suspend execution for 5 seconds
        CD = true;
        yield return new WaitForSeconds(0.75f);
        CD = false;
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
            //----------KEYBOARD-------------
            //int x;
            //int y;
            //if (Input.GetKey("left"))
            //{
            //    x = -1;
            //} else if (Input.GetKey("right"))
            //{
            //    x = 1;
            //} else
            //{
            //    x = 0;
            //}
            //if (Input.GetKey("up"))
            //{
            //    y = -1;
            //    jump(y);
            //}
            //else
            //{
            //    y = 0;
            //}
            //walk(x, y);
            //jump(y);
            //----------KEYBOARD-------------


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
                if (m_Rigidbody.velocity.x == 0)
                {
                    anim.SetBool("standjump", true);
                } else
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

    void walk(float x, float y)
    {
        anim.SetBool("idle", false);
        anim.SetBool("isWalking", true);

        if (x > 0)
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
        else if (x < 0)
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
        Debug.Log(collide.gameObject.tag);
        if (collide.gameObject.tag == "ground")
        {
            grounded = true;
            anim.SetBool("inair", false);
            anim.SetBool("jumping", false);
            anim.SetBool("standjump", false);
        }
    }
}
