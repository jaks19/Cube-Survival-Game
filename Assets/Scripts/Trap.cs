using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {
	public float waitTime = 3.0f;

	// Use this for initialization
	void Start () {
		StartCoroutine (Protrude());
	}

	// Co-routine for spike protrusion
	IEnumerator Protrude(){
		while (true) {
			GetComponent<Animation> ().Play();
			yield return new WaitForSeconds (waitTime);
		}
	}
}