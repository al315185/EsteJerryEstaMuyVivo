using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour {

	//------//------//------//------//------//
	// FOG OF WAR //
//	int visibility;
	//------//------//------//------//------//


    public float x ;
    public float y;

    public int tileX;
    public int tileY;

    private void Start()
    {
        x =  this.transform.position.x;
        y = this.transform.position.z;

    }



    //Look for neightboors


	//------//------//------//------//------//
	// FOG OF WAR //
//	public HexCellShaderData ShaderData{get;set;}
//	public int Index{get; set;}
//
//	public bool IsVisible{
//		get{return visibility > 0; }
//	}
//
//	public void IncreaseVisibility(){
//		visibility += 1;
//		if (visibility == 1) {
//			ShaderData.RefreshVisibility (this);
//		}
//	}
//
//	public void DecreaseVisibility(){
//		visibility -= 1;
//		if (visibility == 0) {
//			ShaderData.RefreshVisibility (this);
//		}
//	}
	//------//------//------//------//------//
}
