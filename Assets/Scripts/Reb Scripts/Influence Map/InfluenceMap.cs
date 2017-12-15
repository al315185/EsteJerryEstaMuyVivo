using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

public class InfluenceMap : MonoBehaviour {
	public GameObject InfluenceMapTexture;
	public GameObject GridPositionObject;
	public List<GameObject> OriginatorObject;
	public LayerMask influenceMask;

	InfluenceGrid iG;

	void Start(){
		iG = new InfluenceGrid ();
		iG.CreateMap (100, 100, 1f, GridPositionObject, true);
		iG.InfluenceMask = influenceMask;
		for (int i = 0; i < OriginatorObject.Count; i++) {
			iG.RegisterOriginator (OriginatorObject [i].GetComponent<Influencer>().originator);
		}
		iG.UpdateMap ();
	}
}
