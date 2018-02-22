using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IzaOni : Izanami {

    string test = "watwtwer";
    static IzaOni s_izaoni;
    static Izanagi m_izanagi;
    new public float speed = 5;
	float decay_amount = 30;
	float time;
	public Image DecayBar;
	public GameObject healthCanvas;

    // Use this for initialization
    void Start () {
        Debug.Log(123);
        m_izanagi = Izanagi.Get();
        form = "ghost";
        following = false;
		healthCanvas.SetActive (true);
    }

    new public static IzaOni Get()
    {
        return s_izaoni;
    }

    public IzaOni()
    {
        s_izaoni = this;

    }
    // Update is called once per frame
    public override void Update () {
        base.Update();
		if (time > 0) {
			time -= Time.deltaTime;
			DecayBar.fillAmount = time / decay_amount;
		}

    }

}
