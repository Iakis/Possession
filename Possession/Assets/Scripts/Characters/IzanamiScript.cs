using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IzanamiScript : MonoBehaviour {

    public float speed = 10;
    float y;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        var x = Input.GetAxis("NamiKeyboardX") * Time.deltaTime * speed;
        transform.Translate(x, 0, 0);
        var z = Input.GetAxis("NamiKeyboardY") * Time.deltaTime * speed * 0.5f;
        transform.Translate(0, 0, z);
    }

}
