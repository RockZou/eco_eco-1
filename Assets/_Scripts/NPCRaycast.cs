using UnityEngine;
using System.Collections;

public class NPCRaycast : MonoBehaviour {

	public float speed = 0.5f;
	public float raycast_range = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Dude AI. Creates a ray from the body of the dude. Checks a short distance ( raycast range) to check for colission
	// If there is a colission set a random rotation
	void FixedUpdate () {

		GetComponent<Rigidbody>().AddForce(-transform.right * speed, ForceMode.VelocityChange );

		Ray ray = new Ray (transform.position, transform.forward);

		if (Physics.Raycast(ray, raycast_range ) ){
			GetComponent<Rigidbody>().velocity = Vector3.zero;
		
			transform.Rotate(0f, Random.Range(0,359), 0f);

		}
	}
}
