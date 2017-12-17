using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCamera : MonoBehaviour {

	//Falta ponerle límites al tamaño del mapa (Cuando se decida)

	// Viewport: The bottom-left of the camera is (0,0); the top-right is (1,1). 
	public float velocity;
	public float xLimit, yLimit;

	void Start () {
	}

	void LateUpdate () {

		float actualX = GetComponent<Camera> ().ScreenToViewportPoint (Input.mousePosition).x;
		float actualY = GetComponent<Camera> ().ScreenToViewportPoint (Input.mousePosition).y;
			
		if (actualX < xLimit) {
			transform.position += Vector3.left * Time.deltaTime * velocity;
		}

		if (actualX > 1 - xLimit) {
			transform.position += Vector3.right * Time.deltaTime * velocity;
		}

		if (actualY < yLimit) {
			transform.position += Vector3.back * Time.deltaTime * velocity;
		}

		if (actualY > 1 - yLimit) {
			transform.position += Vector3.forward * Time.deltaTime * velocity;
		}

	}
}
