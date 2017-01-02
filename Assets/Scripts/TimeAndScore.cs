using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

// This class will be attached to the player to manage the Timer for each level
// 	Also, the current max level reached will be saved in the backend using userprefs
public class TimeAndScore : MonoBehaviour {
	// Init
	public float initialTime; // Placeholder in Unity to specify the total time for a level
	public float leftTime;
	private string timeAnnounced; // We hold the time string here to pass to the textbox
	public Text textboxTime;
	public Dictionary<string, bool> tokensCollected = new Dictionary<string, bool>();

	void Start(){
		leftTime = initialTime;
		tokensCollected.Add("BronzeCoin", false);
		tokensCollected.Add("SilverCoin", false);
		tokensCollected.Add("GoldCoin", false);
	}

	void Update(){
		if (leftTime > 0f) {
			timeAnnounced = "Time: " + string.Format ("{0:0.0}", leftTime);
			leftTime -= Time.deltaTime;
			// If below 5.0s make text red
			if (leftTime <= 5.0f) {
				textboxTime.color = Color.red;
			}
		} else {
			leftTime = 0f;
			timeAnnounced = "Time: " + string.Format ("{0:0.0}", leftTime);
			// Time up so reload level (in future, make a menu appear with continue etc..)
			StartCoroutine(Statics.ReloadLevel());
		}
		// We have already dragged the textbox to this script now puch the value to it
		textboxTime.text = timeAnnounced;
	}

	// When a Coin is collected
	void OnTriggerEnter(Collider other){
		if (other.transform.tag.Contains("Coin")) {
			tokensCollected [other.transform.tag] = true;
			print (other.transform.tag);
			other.gameObject.GetComponent<MeshRenderer> ().enabled = false;
			// Each coin object has a fall effect attached to its 'Coin Rotation' script
			Instantiate (other.transform.gameObject.GetComponent<CoinRotation>().fallEffects, transform.position, Quaternion.identity);
			Destroy (other.gameObject, 0f);
		} 
	}
}