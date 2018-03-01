using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingText : MonoBehaviour {

	Text startGame;
	string blank = "";
	string flashing = "Press space to play";

	// Use this for initialization
	void Start () {
		startGame = GetComponent<Text> ();
		StartCoroutine (BlinkText ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//function to blink the text 
	public IEnumerator BlinkText(){
		//blink it forever. You can set a terminating condition depending upon your requirement
		while (true) {
			startGame.text = blank;
			yield return new WaitForSeconds (.5f);
			startGame.text = flashing;
			yield return new WaitForSeconds (.5f);
		}
	}
}
