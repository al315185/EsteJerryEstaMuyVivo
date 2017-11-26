using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FogMapping : MonoBehaviour {

	public int Seeding;
	public GameObject FogSeed;
	public Transform Ground;

	void Awake(){

		float groundX = Ground.GetComponent<MeshRenderer> ().bounds.size.x;
		float groundY = Ground.GetComponent<MeshRenderer> ().bounds.size.z;

		float seedSizeX = groundX / Seeding;
		float seedSizeY = groundY / Seeding;

		Debug.Log (seedSizeX);
		Debug.Log (seedSizeY);

		for (int i = 0; i < Seeding; i++) {
			for (int j = 0; j < Seeding; j++) {
				GameObject newSeed = Instantiate (FogSeed);

				newSeed.transform.localScale = new Vector3 (seedSizeX * 2, 1f, seedSizeY * 2);
				newSeed.transform.position = new Vector3(-groundX / 2 + (i + 1)* seedSizeX, 20f, -groundY / 2 + (j+1) * seedSizeY);

				newSeed.transform.SetParent (GameObject.Find ("Fog").GetComponent<Transform> ());
			}
		}

		FogSeed.SetActive (false);
	}

	void Start () {
	}
	
	void Update () {
		
	}
}