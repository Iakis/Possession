using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Izanagi : MonoBehaviour {

    public float speed = 10;
    static Izanagi s_izanagi;
    static Izanami s_izanami;
    Rigidbody m_Rigidbody;
    Animator anim;
    Quaternion rot;
    Quaternion rot2;

    public bool follow;
    public bool shielded;


    bool CD;
    bool press;

    public static Izanagi Get()
    {
        return s_izanagi;
    }

    Izanagi()
    {
        s_izanagi = this;
    }
    // Use this for initialization
    void Start () {
        s_izanami = Izanami.Get();
		anim = GetComponent<Animator> ();
        follow = false;
        m_Rigidbody = GetComponent<Rigidbody>();
        rot = transform.rotation;
        rot2 = Quaternion.Inverse(rot);
        CD = false;
        press = false;
        shielded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CD)
        {
            move();
        }
        attack();
        
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

       

        
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
    }

    IEnumerator slice()
    {
        // suspend execution for 5 seconds
        anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.25f);
        anim.SetBool("isAttacking", false);
    }

    IEnumerator cooldown()
    {
        // suspend execution for 5 seconds
        CD = true;
        yield return new WaitForSeconds(0.5f);
        CD = false;
    }

    void move()
    {
        if (!s_izanami.following)
        {
            var x = Input.GetAxis("NagiX") * Time.deltaTime * speed;
            if (x > 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.time * speed);
                anim.SetBool("isWalking", true);
                transform.Translate(0, 0, x);
            }
            else if (x < 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, rot2, Time.time * speed);
                anim.SetBool("isWalking", true);
                transform.Translate(0, 0, -x);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }

        }
        else if (s_izanami.form == "ghost")
        {
            var x = Input.GetAxis("NamiX") * Time.deltaTime * speed;
            transform.Translate(x, 0, 0);
        }
    }
}
