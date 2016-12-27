using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

	// Attach the script to any object which needs to be destroyed upon appearing, after some time t
	// Can be reused many times (e.g. coins can have a lifetime)
	// Using 0 by default but can change in Unity as it is public
	public float lifetime = 0;

	void Start () {
		Destroy (gameObject, lifetime);
	}

} 
