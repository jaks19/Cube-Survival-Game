using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {
	private float maxSpeed = 30.0f;
	public float thrust; // Multiplier for the value from keypress
	private Vector3 inputVector; // Will hold the vector user wants to move in
	private Rigidbody rb; // Forces will act upon the rigidbody to make the player move
	public GameObject sparks; // For when the player hits a bomb
	public GameObject fallEffects;
	private Vector3 respawnLocation; // The position to which we need to respawn. initialize it in Start()
	private bool acceptInputs; // Flag saying if we accept inputs or not
	private bool dieEffects; // Flag allowing effects to show or not

	// Use this for initialization
	void Start () {
		dieEffects = true;
		respawnLocation = transform.position;
		acceptInputs = true;
		PlayerPrefs.SetInt ("current", SceneManager.GetActiveScene ().buildIndex);

		// If first time we reach this level, raise the maxLevelReached bar 
		// Maybe use this bar later to know if have access to a level or not etc
		if (SceneManager.GetActiveScene ().buildIndex > PlayerPrefs.GetInt ("max")) {
			PlayerPrefs.SetInt ("max", SceneManager.GetActiveScene ().buildIndex);
		}
	}
		
	void FixedUpdate () {
		// Place user's desired direction in this Vectror3
		inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		rb = GetComponent<Rigidbody> ();
		if (rb.velocity.magnitude < maxSpeed && acceptInputs) {
			rb.AddRelativeForce(inputVector * thrust);
		}
		if (rb.transform.position.y < 0) {
			Die("Fall");
		}
	}

	// Deals with the case when this GO collides
	void OnTriggerEnter(Collider other){
		if (other.transform.tag == "Goal") {
			// Level Completed
			TimeAndScore TimeAndScore = gameObject.GetComponent<TimeAndScore> ();
			Dictionary<string, bool> tokensCollected = TimeAndScore.tokensCollected;
			float leftTime = TimeAndScore.leftTime;
			float initialTime = TimeAndScore.initialTime;
			float score = Statics.CalculateScore (tokensCollected, leftTime, initialTime);
			print (score);
			// Load next level
			SceneManager.LoadScene (PlayerPrefs.GetInt ("current") + 1);
		}
		// We tagged enemies so that we know that it is enemies that we are colliding with
		else if (other.transform.tag == "Enemy") {
			Die ("Bomb");
		}
	}

	// Defined for modularizing death and respawning (effects plus reappearance of player)
	void Die(string whyDead){
		if (dieEffects) {
			// Suspend Movements and Disappear
			gameObject.GetComponent<MeshRenderer> ().enabled = false;
			gameObject.GetComponent<BoxCollider> ().enabled = false;
			// Make effects appear
			if (whyDead == "Fall") {
				Instantiate (fallEffects, transform.position, Quaternion.identity);
				dieEffects = false;
			} else if (whyDead == "Bomb") {
				Instantiate (sparks, transform.position, Quaternion.identity);
				dieEffects = false;
			}
		}
		StartCoroutine(Statics.ReloadLevel());
	}

	// We will check if the player loses contact with the floor
	// 	Then we will suspend inputs, until we respawned fully
	void OnCollisionEnter (Collision other) 
	{
		if (other.transform.tag == "Floor") {
			acceptInputs = true;
		}
	}
	void OnCollisionExit (Collision other) 
	{
		if (other.transform.tag == "Floor") {
			acceptInputs = false;
		}
	}
}