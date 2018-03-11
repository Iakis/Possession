using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPossessPrompt : MonoBehaviour {

	[SerializeField] private Image currentPrompt;
	public Sprite attackPrompt;
	public Sprite possessPrompt;
	public GameObject Oni;
	private OniAI oniScript;

	void Start(){
		currentPrompt.enabled = false;
		oniScript = Oni.GetComponent<OniAI> ();
	}

	// Use this for initialization
	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			// Check Oni health is 0
			Debug.Log("Enter prompt");
			if(oniScript.health > 0){
				currentPrompt.sprite = attackPrompt;
			}
			else{
				currentPrompt.sprite = possessPrompt;
			}
			currentPrompt.enabled = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.CompareTag ("Player")) {
			Debug.Log("Exit prompt");
			currentPrompt.enabled = false;
		}
	}

	void Update() {
	}
		
}
