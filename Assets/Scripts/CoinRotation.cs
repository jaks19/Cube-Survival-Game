using UnityEngine;
using System.Collections;

public class CoinRotation : MonoBehaviour

{
	private float rotateSpeed = 2.3f;
	public GameObject fallEffects;

	void Update () {
		transform.Rotate(Vector3.up * rotateSpeed, Space.World);
		transform.Rotate(Vector3.right * rotateSpeed, Space.World);
	}
}

