﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptedMap : MonoBehaviour
{
	public GameObject selectedUnit;
	public TileType[] tileTypes;
	Node[,] graph;

    public Hex[,] map;

	public int[,] tiles;
	public int height = 40;
	public int width = 60;

	float xOffset = 1.77f;
	float zOffset = 1.51f;

	void Awake(){

    }


	void Start () {
        map = new Hex[width, height];
        GenerateDataInfo(gameObject);
        selectedUnit = null;
        GenerateMapData();
		GeneratePathFindgGraph();
		//GenerateMapVisual();
	}

    private void GenerateDataInfo(GameObject mapa)
    {
        for(int i=0; i < gameObject.transform.childCount; i++)
        {
            Hex h = gameObject.transform.GetChild(i).GetComponent<Hex>();
            map[h.tileX, h.tileY] = h;
        }
    }

    void Update () {

		if (selectedUnit == null)
			return;
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				if(selectedUnit.transform.position == map[x, y].transform.position)
				{
					selectedUnit.GetComponent<Pathfinding>().tileX = map[x, y].tileX;
					selectedUnit.GetComponent<Pathfinding>().tileY = map[x, y].tileY;
				}
			}
		}
	}

	void GenerateMapData()
	{
		tiles = new int[width, height];
		//map = new Hex[width, height];

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				tiles[x, y] = 0;
			}
		}

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

	public float CostToEnterTile (int sourceX, int sourceY, int targetX, int targetY)
	{
		TileType tt = tileTypes[tiles[targetX, targetY]];

		if (UnitCanEnterTile(targetX, targetY) == false)
			return Mathf.Infinity;
		float cost = tt.momevementCost;

		if (sourceX != targetX && sourceY != targetY)
		{
			cost += 0.001f;
		}

		return cost;
	}
		
	void GeneratePathFindgGraph()
	{
		graph = new Node[width, height];

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				graph[x, y] = new Node();
				graph[x, y].x = x;
				graph[x, y].y = y;
			}
		}

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				//if pair row
				if (y % 2 == 0)
				{
					//left neighbours
					if (x > 0)
					{
						graph[x, y].neighbours.Add(graph[x - 1, y]);
						if (y > 0)
							graph[x, y].neighbours.Add(graph[x - 1, y - 1]);
						if (y < height - 1)
							graph[x, y].neighbours.Add(graph[x - 1, y + 1]);

					}
					//Right neighbours
					if (x < width - 1)
						graph[x, y].neighbours.Add(graph[x + 1, y]);
					if (y > 0)
						graph[x, y].neighbours.Add(graph[x, y - 1]);
					if (y < height - 1)
						graph[x, y].neighbours.Add(graph[x, y + 1]);

				}

				//odd row
				else
				{
					//left neighbours
					if (x > 0)
						graph[x, y].neighbours.Add(graph[x - 1, y]);
					if (y > 0)
						graph[x, y].neighbours.Add(graph[x, y - 1]);
					if (y < height - 1)
						graph[x, y].neighbours.Add(graph[x, y + 1]);

					//Right neighbours
					if (x < width - 1)
					{
						graph[x, y].neighbours.Add(graph[x + 1, y]);
						if (y > 0)
							graph[x, y].neighbours.Add(graph[x + 1, y - 1]);
						if (y < height - 1)
							graph[x, y].neighbours.Add(graph[x + 1, y + 1]);
					}
				}
			}
		}
	}

	void GenerateMapVisual()
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				float xPos = x * xOffset;

				if (y % 2 == 1)
				{
					xPos += xOffset / 2f;
				}
				TileType tt = tileTypes[tiles[x, y]];
				GameObject hex_go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(xPos, 0, y * zOffset), Quaternion.identity);
				hex_go.name = "Hex_" + x + "_" + y;

				hex_go.GetComponent<Hex>().x = x;
				hex_go.GetComponent<Hex>().y = y;

				hex_go.GetComponent<Hex>().tileX = x;
				hex_go.GetComponent<Hex>().tileY = y;
				map[x, y] = hex_go.GetComponent<Hex>();

				hex_go.transform.SetParent(this.transform);

			}
		}
	}
	public Vector3 TileCoordToWorldCoord(int x, int y)
	{
		return new Vector3(map[x,y].x, 0,map[x,y].y);
	}

	public bool UnitCanEnterTile(int x, int y)
	{
		return tileTypes[tiles[x, y]].isWalkable;
	}

	public void GeneratePathTo(int x, int y)
	{
		if (selectedUnit == null)
			return;

		selectedUnit.GetComponent<Pathfinding> ().currentPath = null;


		Dictionary<Node, float> dist = new Dictionary<Node, float>();
		Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

		List<Node> unvisited = new List<Node>();

		Node source = graph[selectedUnit.GetComponent<Pathfinding>().tileX,selectedUnit.GetComponent<Pathfinding>().tileY];
		Node target = graph[x, y];

		dist[source] = 0;
		prev[source] = null;

		foreach (Node v in graph)
		{
			if (v != source)
			{
				dist[v] = Mathf.Infinity;
				prev[v] = null;
			}
			unvisited.Add(v);
		}

		while (unvisited.Count > 0)
		{

			Node u = null;

			foreach (Node possibleU in unvisited)
			{
				if ( u == null || dist[possibleU] < dist[u])
				{
					u = possibleU;
				}
			}

			if (u == target)
			{
				break;
			}

			unvisited.Remove(u);

			foreach (Node v in u.neighbours)
			{
				float alt = dist[u] +  CostToEnterTile(u.x,u.y,v.x,v.y);
				if(alt < dist[v])
				{
					dist[v] = alt;
					prev[v] = u;
				}
			}

		}

		if(prev[target] == null)
		{
			return;
		}

		List<Node> currentPath = new List<Node>();

		Node curr = target;

		while (curr != null)
		{
			currentPath.Add(curr);
			curr = prev[curr];
		}

		currentPath.Reverse();

		selectedUnit.GetComponent<Pathfinding>().currentPath = currentPath;
	}
}


