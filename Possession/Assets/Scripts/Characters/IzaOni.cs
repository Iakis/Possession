using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IzaOni : Izanami {

    string test = "watwtwer";
    static IzaOni s_izaoni;
    static Izanagi m_izanagi;

    bool opress;
    bool oCD;
    //Rigidbody m_Rigidbody;
    Animator oanim;

    // Use this for initialization
    public override void Start () {
        base.Start();
        opress = false;
        oCD = false;
        //m_izanagi = Izanagi.Get();
        //form = "ghost";
        //following = false;
        //m_Rigidbody = GetComponent<Rigidbody>();
        oanim = GetComponent<Animator>();
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
        yield return new WaitForSeconds(1f);
        oanim.SetBool("smash", false);
    }

    IEnumerator cooldown()
    {
        // suspend execution for 5 seconds
        oCD = true;
        yield return new WaitForSeconds(3);
        oCD = false;
    }

    public IzaOni()
    {
        s_izaoni = this;

    }
    // Update is called once per frame
    public override void Update () {
        base.Update();

    }

}
