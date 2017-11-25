using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogMapping : MonoBehaviour {

	public int BoxSize;
	public GameObject FogBox;
	public Transform Ground;

	private GameObject[,] fogGreed;

	void Start () {

		transform.localScale = Ground.localScale;
		int f = BoxSize * (int)transform.localScale.x;
		int c = BoxSize * (int)transform.localScale.z;

		fogGreed = new GameObject[f, c];

		for (int i = 0; i < fogGreed.GetLength (0); i++) {
			for (int j = 0; j < fogGreed.GetLength (1); j++) {
				GameObject fogBox = Instantiate (FogBox);
				fogBox.transform.localScale = new Vector3 (fogBox.transform.localScale.x /BoxSize, 1f, fogBox.transform.localScale.z/BoxSize);

//
//				fogBox.transform.position = new Vector3 (0 -transform.localScale.x * 2 + i * fogBox.transform.localScale.x, 
//					transform.position.y, 0-transform.localScale.z * 2 + j * fogBox.transform.localScale.z );
				fogGreed [i, j] = fogBox;
			}
		}
	}
	
	void Update () {
		
	}
}