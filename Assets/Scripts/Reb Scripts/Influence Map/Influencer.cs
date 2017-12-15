using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

public class Influencer : MonoBehaviour {
	public Originator originator;
	void Update(){
		originator.worldPosition = transform.position;
	}
}
