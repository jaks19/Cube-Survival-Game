using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {
	// Attach the script to any object which needs to be destroyed upon appearing, after some time t
	// Using 0 by default but can change in Unity as it is public
	// Used so far for:
	// 	- Sparks

	public float lifetime = 0;

	void Start () {
		Destroy (gameObject, lifetime);
	}
} 
