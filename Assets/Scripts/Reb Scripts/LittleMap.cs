using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleMap : MonoBehaviour {

	Camera mapCamera;
	// Use this for initialization
	void Start () {
		mapCamera = transform.GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hitInfo;

		if (Physics.Raycast(ray, out hitInfo))
		{
			GameObject hitObject = hitInfo.collider./*transform.parent.*/gameObject;

			if (Input.GetMouseButton (0)) {
				
				Debug.Log(mapCamera.ViewportToScreenPoint (mapCamera.ScreenToViewportPoint (hitInfo.transform.position)));
			}
		}
	}
}
