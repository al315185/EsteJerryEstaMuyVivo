using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public GameObject selectedUnit;
    public TileType[] tileTypes;

    int[,] tiles;
    int height = 20;
    int width = 20;

    float xOffset = 1.77f;
    float zOffset = 1.51f;


	// Use this for initialization
	void Start () {
        selectedUnit = null;

        GenerateMapData();
        GenerateMapVisual();
        
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void GenerateMapData()
    {
        tiles = new int[width, height];

        //initializate all mapa as normal tile

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = 0;
            }
        }
        // Make a big swamp area
        for (int x = 3; x <= 5; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                tiles[x, y] = 1;
            }
        }

        tiles[4, 4] = 2;
        tiles[5, 4] = 2;
        tiles[6, 4] = 2;
        tiles[7, 4] = 2;
        tiles[8, 4] = 2;

        tiles[4, 5] = 2;
        tiles[4, 6] = 2;
        tiles[8, 5] = 2;
        tiles[8, 6] = 2;

    }
    void GenerateMapVisual()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xPos = x * xOffset
                    ;

                if (y % 2 == 1)
                {
                    xPos += xOffset / 2f;
                }
                TileType tt = tileTypes[tiles[x, y]];
                GameObject hex_go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(xPos, 0, y * zOffset), Quaternion.identity);
                hex_go.name = "Hex_" + x + "_" + y;
                hex_go.GetComponent<Hex>().x = x;
                hex_go.GetComponent<Hex>().y = y;

                hex_go.transform.SetParent(this.transform);
            }
        }
    }
    public Vector3 TileCoordToWorldCoord(float x, float y)
    {
        return new Vector3(x, 0, y);
    }

    public void MoveSelectUnitTo(float x, float y)
    {
        if (selectedUnit == null)
            return;

        selectedUnit.GetComponent<Unit>().tileX = x;
        selectedUnit.GetComponent<Unit>().tileY = y;
        selectedUnit.transform.position = TileCoordToWorldCoord(x, y);
    }
}
