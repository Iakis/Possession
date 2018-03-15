using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IzaOni : Izanami {

    string test = "watwtwer";
    static IzaOni s_izaoni;
    static Izanagi m_izanagi;
    GameObject m_axe;

    bool opress;
    bool oCD;
    bool grounded;
    bool reviving;
    float currentTime = 0f;
    float timeToMove = 2f;
    Rigidbody o_Rigidbody;
    Animator oanim;
    Vector3 end;
    public Quaternion orot;
    public Quaternion orot2;
    AudioSource axeSound;

    // Use this for initialization
    public override void Start () {
        base.Start();
        this.gameObject.AddComponent<Rigidbody>();
        opress = false;
        oCD = false;
        //m_izanagi = Izanagi.Get();
        //form = "ghost";
        //following = false;
        //o_Rigidbody = GetComponent<Rigidbody>();
        this.gameObject.layer = 0;
        oanim = GetComponent<Animator>();
        grounded = true;
        m_axe = GameObject.Find("Axe");
        end = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        reviving = true;

        orot.eulerAngles = new Vector3(0f, 90f, 0f);    
        orot2.eulerAngles = new Vector3(0f, -90f, 0f);
        axeSound = GameObject.Find("AxeSound").GetComponent<AudioSource>();
        //if (this.transform.rotation == orot2)
        //{
        //    orot.eulerAngles = new Vector3(0, -90, 0);
        //    orot2.eulerAngles = new Vector3(0, 90, 0);
        //}
    }

    new public static IzaOni Get()
    {
        return s_izaoni;
    }

    public override void attack()
    {
        if (Input.GetAxis("NamiAttack") != 0)
        {
            opress = true;
        }
        else if (Input.GetAxis("NamiAttack") == 0)
        {
            if (opress == true)
            {
                if (!oCD)
                {
                    StartCoroutine("smash");
                    StartCoroutine("cooldown");
                }
                opress = false;
            }
        }
    }

    IEnumerator smash()
    {
        m_axe.GetComponent<BoxCollider>().enabled = true;
        oanim.SetTrigger("attack");
        yield return new WaitForSeconds(0.5f);
        m_axe.GetComponent<Axe>().isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("playAxe");
        m_axe.GetComponent<Axe>().isAttacking = false;
        yield return new WaitForSeconds(1f);
        m_axe.GetComponent<BoxCollider>().enabled = false;
    }

    IEnumerator playAxe()
    {
        yield return new WaitForSeconds(0.5f);
        axeSound.Play();
    }

    IEnumerator cooldown()
    {
        // suspend execution for 5 seconds
        oCD = true;
        yield return new WaitForSeconds(1.5f);
        oCD = false;
    }

    public IzaOni()
    {
        s_izaoni = this;

    }
    // Update is called once per frame
    public override void Update () {
        
        if (reviving)
        {
            rise();
        } else
        {
            //o_Rigidbody.constraints = RigidbodyConstraints.None;
            //o_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            oanim.SetTrigger("revive");
            base.Update();
            unpossess();
        }

    }

    public override void move()
    {
        var x = Input.GetAxis("NamiX");
        var y = Input.GetAxis("NamiY");
        
        
        base.walk(x, y, orot, orot2);
        base.jump(y);
    }
    public void rise()
    {
        if (currentTime <= timeToMove)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, end, currentTime / timeToMove);
        }
        else
        {
            
            o_Rigidbody = gameObject.GetComponent<Rigidbody>();
            transform.position = end;
            currentTime = 0f;
            reviving = false;
            o_Rigidbody.isKinematic = false;
        }

    }

    void unpossess()
    {
        if (Input.GetButtonUp("NamiInteract"))
        {
            oanim.SetTrigger("die");
            GameObject go = (GameObject)Instantiate(Resources.Load("Izanami", typeof(GameObject)), this.gameObject.transform.position, this.gameObject.transform.rotation);
            this.gameObject.GetComponent<IzaOni>().enabled = false;
            this.gameObject.GetComponent<OniAI>().enabled = true;
            m_axe.GetComponent<BoxCollider>().enabled = false;
        }
    }

}
