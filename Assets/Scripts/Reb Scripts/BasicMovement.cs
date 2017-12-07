using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		float x = Input.GetAxis ("Horizontal");
		float z = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (x * speed * Time.deltaTime, 0f, z * speed * Time.deltaTime);
		transform.position += movement;

		RaycastHit hit;

		if (Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward, out hit)) {
			Debug.Log ("entro");
			if (hit.collider.name.Equals("Fog(Clone)")) {
				Destroy (hit.collider.gameObject);
			}
		}

	}

	void OnCollisionEnter(Collision c){
		if (c.collider.tag.Equals("Fog")) {
			Destroy (c.gameObject);
		}
	}
}
