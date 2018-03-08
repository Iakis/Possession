using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OniAI : MonoBehaviour
{
    private bool dead = false;
    ParticleSystem pSys;
    private Shader originShader;
    public Shader myShader;
    private Renderer rend;

    public int health = 0;
    Transform m_axe;
    Animator anim;
    Vector3 end;
    float currentTime = 0f;
    float timeToMove = 2f;
    Rigidbody m_rigidbody;
    bool canhit;
    static Izanagi s_izanagi;
    static Izanami s_izanami;
    public float range = 20f;
    bool detected;
    Vector3 target;


    // Use this for initialization
    void Start()
    {
        s_izanami = Izanami.Get();
        s_izanagi = Izanagi.Get();
        pSys = transform.Find("FallingPaper").GetComponent<ParticleSystem>();
        pSys.Stop();
        anim = this.gameObject.GetComponent<Animator>();
        m_rigidbody = this.gameObject.GetComponent<Rigidbody>();
        end = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        canhit = true;
        detected = false;
        m_axe = transform.GetChild(2).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
        m_axe.GetComponent<BoxCollider>().enabled = false;
        originShader = Shader.Find("Hand-Drawn/Fill+Outline/Simple Outline + Bumped Diffuse Fill");
        rend = transform.GetChild(0).GetComponent<Renderer>();
        myShader = Shader.Find("Outlined/Silhouetted Diffuse");
    }

    // Update is called once per frame
    void Update()
    {
        if (s_izanami == null || s_izanami.gameObject.layer != 8)
        {
            s_izanami = Izanami.Get();
        }
        if (s_izanagi == null) s_izanagi = Izanagi.Get();
        if (dead)
        {
            //m_rigidbody.isKinematic = true;
            fall();
            if (Math.Abs(s_izanami.transform.position.x - transform.position.x) < 5f)
            {
                rend.material.shader = myShader;
            } else
            {
                rend.material.shader = originShader;
            }
            return;
        } else
        {
            if (!detected)
            {
                StartCoroutine("DetectPlayer");
            }
            else
            {
                float dNami = s_izanami.transform.position.x - transform.position.x;
                float dNagi = s_izanagi.transform.position.x - transform.position.x;
                float dMin = Math.Min(Math.Abs(dNami), Math.Abs(dNagi));
                if (dMin < 7)
                {
                    StartCoroutine("smash");
                }
                else
                {
                    detected = false;

                }
            }
        }
    }

    IEnumerator smash()
    {
        m_axe.GetComponent<BoxCollider>().enabled = true;
        anim.SetBool("smash", true);
        yield return new WaitForSeconds(1f);
        m_axe.GetComponent<BoxCollider>().enabled = false;
        anim.SetBool("smash", false);
    }

    IEnumerator DetectPlayer()
    {
        float dNami = s_izanami.transform.position.x - transform.position.x;
        float dNagi = s_izanagi.transform.position.x - transform.position.x;
        float dMin = Math.Min(Math.Abs(dNami), Math.Abs(dNagi));
        if (dMin < range)
        {
            Vector3 offset;
            if (Math.Abs(dNami) < Math.Abs(dNagi))
            {
                if (dNami > 0) offset = Vector3.left * 5;
                else offset = Vector3.right * 5;
                float step = 5f * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, s_izanami.transform.position+offset, step);
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
                transform.position = Vector3.MoveTowards(transform.position, s_izanagi.transform.position+offset, step);
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
        m_axe.GetComponent<BoxCollider>().enabled = false;
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
        //Debug.Log("asddsfga");
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
