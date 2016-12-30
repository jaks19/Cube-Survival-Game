using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Protrude());
	}

	// Co-routine for spike protrusion
	IEnumerator Protrude(){
		while (true) {
			GetComponent<Animation> ().Play();
			yield return new WaitForSeconds (3.0f);
		}
	}
}