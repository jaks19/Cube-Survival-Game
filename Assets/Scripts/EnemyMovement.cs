using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public Transform[] transforms; // A public array of transforms (public so we an just drag and drop transforms that we want into it)
	public float speed;
	private Transform desiredNewTransform; // Always the next position we want to be at

	// We want the enemy to be at transform[0], then [1] etc and when it reaches the end, it should move again to [0]:-
	private int startIndex = 0;
	private int index;

	void Start () {
		desiredNewTransform = transforms [startIndex];
	}

	void Update () {
		// If we reached our designated destination, keep picking the next 
		if (transform.position == desiredNewTransform.position) {
			// One case is that we are still well within the bounds of the array:
			if (index < transforms.Length - 1) {
				index++;
			} else {
				index = 0;
			}
			// No matter what, change the position because reached dest
			desiredNewTransform = transforms[index];
		}
		// Update location in every frame! (Should be an ongoing process)
		// Need to move this object (enemy) to that point:
		transform.position = Vector3.MoveTowards(transform.position, desiredNewTransform.position, speed * Time.deltaTime);
	}
}