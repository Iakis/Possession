using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oni1Script : MonoBehaviour {

	Renderer rend;
	private bool dead = false;
	Animator anim;

	[Header("Oni Stats")]
	public int health = 25;

	[Header("Oni Materials")]
	public Material AliveMaterial;
	public Material CorpseMaterial;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rend = GetComponent<Renderer> ();
		rend.enabled = true;
		rend.material = AliveMaterial;
	}
	
	// Update is called once per frame
	void Update () {
		if (dead) {
			return;
		}
	}

	void Damage(int amount){
		health -= amount;

		if (health <= 0) {
			Die ();
		}
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.layer != 8) {
			Damage (5);
			if (dead) {
				if (collision.gameObject.layer == 9) {
					
					collision.gameObject.GetComponent<Oni1Script>().EnemyIsDead ();
				}
			}
		}
	}

	public void Die(){
		dead = true;
		anim.SetBool ("isDead", true);
		rend.material = CorpseMaterial;
	}

	public void EnemyIsDead(){
		anim.SetBool ("enemyDead", true);
        this.gameObject.layer = 10;
	}
}
