using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptAppear : MonoBehaviour {

	[SerializeField] private Image currentPrompt;
	public Sprite prompt;

	void Start(){
		currentPrompt.sprite = prompt;
		currentPrompt.enabled = false;
	}

	// Use this for initialization
	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			Debug.Log("Enter prompt");
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
