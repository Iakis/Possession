using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OniAI : MonoBehaviour
{
    private bool dead = false;
    ParticleSystem pSys;


    public int health = 20;

    Animator anim;
    Vector3 end;
    float currentTime = 0f;
    float timeToMove = 2f;
    Rigidbody m_rigidbody;
    bool canhit;
    public Transform Izanami;
    public Transform Izanagi;
    public float range = 20f;
    bool detected;
    Vector3 target;

    // Use this for initialization
    void Start()
    {
        Izanami = GameObject.Find("Izanami").transform;
        Izanagi = GameObject.Find("Izanagi").transform;
        pSys = transform.Find("FallingPaper").GetComponent<ParticleSystem>();
        pSys.Stop();
        anim = this.gameObject.GetComponent<Animator>();
        m_rigidbody = this.gameObject.GetComponent<Rigidbody>();
        end = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        canhit = true;
        detected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!detected)
        {
            StartCoroutine("DetectPlayer");
        }
        if (dead)
        {
            m_rigidbody.isKinematic = true;
            fall();
            return;
        }

    }

    IEnumerator DetectPlayer()
    {
        float dNami = Izanami.position.x - transform.position.x;
        float dNagi = Izanagi.position.x - transform.position.x;
        float dMin = Math.Min(Math.Abs(dNami), Math.Abs(dNagi));
        if (dMin < range)
        {
            Vector3 offset;
            if (Math.Abs(dNami) < Math.Abs(dNagi))
            {
                if (dNami > 0) offset = Vector3.left * 5;
                else offset = Vector3.right * 5;
                float step = 5f * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, Izanami.position+offset, step);
                anim.SetBool("walking", true);
                float time = (Math.Abs(dNami) - offset.x) / 5;
                yield return new WaitForSeconds(time);
                anim.SetBool("walking", false);
            }
            else
            {
                if (dNagi > 0) offset = Vector3.left * 5;
                else offset = Vector3.right * 5;
                float step = 5f * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, Izanagi.position+offset, step);
                anim.SetBool("walking", true);
                float time = (Math.Abs(dNagi) - offset.x) / 5;
                yield return new WaitForSeconds(time);
                anim.SetBool("walking", false);
            }
            detected = true;
        }
    }

    IEnumerator die()
    {
        
        this.gameObject.layer = 10;
        this.GetComponent<CapsuleCollider>().isTrigger = true;
        anim.SetBool("die", true);
        yield return new WaitForSeconds(1f);
        dead = true;
    }

    IEnumerator hit()
    {
        canhit = false;
        anim.SetBool("hit", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("hit", false);
        canhit = true;
    }

    void Damage(int amount)
    {
        StartCoroutine("hit");
        health -= amount;
        Debug.Log(string.Format("{0} have been damaged", this.gameObject.name));
        StartCoroutine(PaperFall());
        if (health <= 0)
        {
            StartCoroutine("die");
            fall();
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "attack")
        {
            if (canhit)
            {
                Damage(5);
            }
            if (dead)
            {
                if (collision.gameObject.layer == 9)
                {

                    collision.gameObject.GetComponent<Oni1Script>().EnemyIsDead();
                }
            }
        }
        if (collision.gameObject.name == "sword")
        {
            
            Damage(5);
            gameObject.transform.Translate(0.25f, 0, 0);
            if (dead)
            {
                if (collision.gameObject.layer == 9)
                {

                    collision.gameObject.GetComponent<Oni1Script>().EnemyIsDead();
                }
            }
        }
    }

    public void fall()
    {
        
        if (currentTime <= timeToMove)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, end, currentTime/timeToMove);
        }
        else
        {
            transform.position = end;
            currentTime = 0f;
        }

    }

    IEnumerator PaperFall()
    {
        pSys.Play();
        yield return new WaitForSeconds(1);
        pSys.Stop();
    }

}
