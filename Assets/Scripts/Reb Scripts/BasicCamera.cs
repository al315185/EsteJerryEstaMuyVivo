using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCamera : MonoBehaviour {

	public float offset;
	private Transform player;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Unit").GetComponent<Transform> ();
	}
	
	void Update () {
		transform.position = player.position + Vector3.up * offset;
	}
}
