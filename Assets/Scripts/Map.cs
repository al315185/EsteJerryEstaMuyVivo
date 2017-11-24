using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public GameObject HexPrefab;

    int height = 20;
    int width = 20;

    float xOffset = 1.77f;
    float zOffset = 1.51f;


	// Use this for initialization
	void Start () {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xPos = x*xOffset
                    ;

                if( y % 2 == 1)
                {
                    xPos += xOffset/2f;
                }
                GameObject hex_go = (GameObject)Instantiate(HexPrefab, new Vector3(xPos, 0, y*zOffset), Quaternion.identity);
                hex_go.name = "Hex_" + x + "_" + y;

                hex_go.transform.SetParent(this.transform);
            }
        }
        

        
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
