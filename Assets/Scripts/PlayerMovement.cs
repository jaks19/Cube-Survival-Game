using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	private float maxSpeed = 30.0f;

	// Once inputs have been set to keys and labelled in Unity, we can get their level of activity from -1.0 to 0 (for -ve input)
	// 	and 0 to 1.0 (for +ve input) where magnitudes depend on degree of pressing that button

	// We do not use that number directly, we usually multiply by a speed that we set and here we will make it public
	public float thrust;

	// Anyway to get the input degrees, we refer to them by their names used in Unity and put them this a vector
	private Vector3 inputVector;

	private Rigidbody rb;

	// We create the public variable to hold the sparks GO. We instantiate if later when a collision occurs
	public GameObject sparks;

	// The position to which we need to respawn. initialize it in Start()
	private Vector3 respawnLocation;

	// Use this for initialization
	void Start () {
		respawnLocation = transform.position;
	}

	// Update is called once per frame
	void FixedUpdate () {

		// This is how we put them inside this vector
		inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

		// Because our player has a rigid body component, we can treat it as a rigid body to which forces can be applied
		// 	and that body may have a velocity etc. So to move it, use addForce
		// 	(I can just refer to the rigid body like this:)
		rb = GetComponent<Rigidbody> ();

		if (rb.velocity.magnitude < maxSpeed) {
			rb.AddRelativeForce(inputVector * thrust);
		}

		if (rb.transform.position.y < 0) {
			StartCoroutine(Die());
		}
	}

	// Deals with the case when this GO collides (Runs once only, even of this GO stays stuck to the other GO)
	// 	If needed the function to keep running as long as the bodies touch, would use OnCollisionStay)
	void OnCollisionEnter(Collision other){
		// We tagged enemies so that we know that it is enemies that we are colliding with
		// Can grab the tag of the otherGO from its transform or its collider etc, here will grab from its transform
		if (other.transform.tag == "Enemy") {
			// We make sparks appear where player was and player must respawn to starting position
			// By now, variable sparks contains the sparks GO that we dragged to this GO
			StartCoroutine(Die());

			// Remember to attach a Destroy script to the sparks, otherwise they stay in memory (seen in hierarchy window)
		}

		if (other.transform.tag == "Goal") {
			MySceneManager.PromoteLevel ();
		}
	}

	// Defined by myself for modularizing collision and respawning
	IEnumerator Die(){
		Instantiate(sparks, transform.position, Quaternion.identity);
		GetComponent<MeshRenderer> ().enabled = false;
		yield return new WaitForSeconds (0.4f);
		GetComponent<MeshRenderer> ().enabled = true;
		transform.position = respawnLocation;
	}
}
