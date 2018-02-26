using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IzanamiScript : MonoBehaviour {

    public float speed = 5;
    private Rigidbody rb;
    public float jumpSpeed = 100;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        var x = Input.GetAxis("NamiKeyboardX") * Time.deltaTime * speed;
        transform.Translate(x, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(string.Format("collide with {0}", collision.gameObject.name));
    }

}
