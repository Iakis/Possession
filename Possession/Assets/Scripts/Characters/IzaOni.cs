using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IzaOni : Izanami {

    string test = "watwtwer";
    static IzaOni s_izaoni;
    static Izanagi m_izanagi;
    static Axe m_axe;

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

    // Use this for initialization
    public override void Start () {
        base.Start();
        opress = false;
        oCD = false;
        //m_izanagi = Izanagi.Get();
        //form = "ghost";
        //following = false;
        o_Rigidbody = GetComponent<Rigidbody>();
        
        oanim = GetComponent<Animator>();
        grounded = true;
        m_axe = Axe.Get();
        end = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        reviving = true;

        orot.eulerAngles = new Vector3(0f, 90f, 0f);
        orot2.eulerAngles = new Vector3(0f, -90f, 0f);
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
        oanim.SetBool("smash", true);
        yield return new WaitForSeconds(0.5f);
        m_axe.isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        m_axe.isAttacking = false;
        oanim.SetBool("smash", false);
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
            oanim.SetBool("revive", false);
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
            oanim.SetBool("die",true);
            GameObject go = (GameObject)Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Izanami.prefab", typeof(GameObject)), this.gameObject.transform.position, this.gameObject.transform.rotation);
            this.gameObject.GetComponent<IzaOni>().enabled = false;
            this.gameObject.GetComponent<OniAI>().enabled = true;
        }
    }

}
