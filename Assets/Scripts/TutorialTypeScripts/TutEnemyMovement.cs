using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	// Each object has a 'Transform' which basically is an object aving its location
	// 	We can also have transforms just like that, not attached to any object, and these serve as location markers

	// A public array of transforms (public so we an just drag and drop transforms that we want into it):
	public Transform[] transforms;
	public float speed;
	private Transform desiredNewTransform;
	private int startIndex = 0;

	// We want the enemy to be at transform[0], then [1] etc and when it reaches the end, it should move again to [0]:-
	private int index;

	void Start () {
		// Start at index startIndex
		desiredNewTransform = transforms [startIndex];
	}

	void Update () {
		// Also, we are attaching this script to the enemy so it has its own transform which we can refer to as 'transform' directly
		// 	As if this.transform

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
