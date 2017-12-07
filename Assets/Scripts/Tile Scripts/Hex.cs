using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour {

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
}
