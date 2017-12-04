using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public GameObject selectedUnit;
    public TileType[] tileTypes;
    Node[,] graph;

    Hex[,] map;

    int[,] tiles;
    int height = 20;
    int width = 20;

    float xOffset = 1.77f;
    float zOffset = 1.51f;


	// Use this for initialization
	void Start () {
        selectedUnit = null;

        GenerateMapData();
        GeneratePathFindgGraph();
        GenerateMapVisual();
        
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void GenerateMapData()
    {
        tiles = new int[width, height];
        map = new Hex[width, height];

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

    public class Node
    {
        public List<Node> neighbours;
        public int x;
        public int y;

        public Node()
        {
            neighbours = new List<Node>();
        }

        public float DistanceTo(Node n)
        {
            return Vector2.Distance(new Vector2(x, y), new Vector2(n.x, n.y));
        }
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

    public void GeneratePathTo(int x, int y)
    {
        if (selectedUnit == null)
            return;

        /*selectedUnit.GetComponent<Unit>().tileX = x;
        selectedUnit.GetComponent<Unit>().tileY = y;
        selectedUnit.transform.position = TileCoordToWorldCoord(x, y);*/


        //clear old path;

        selectedUnit.GetComponent<Unit>().currentPath = null;

        //Create dictionary for dikjstra 

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        //Setup Q --> list of unvisited nodes

        List<Node> unvisited = new List<Node>();

        Node source = graph[selectedUnit.GetComponent<Unit>().tileX,selectedUnit.GetComponent<Unit>().tileY];

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
            // "u" is the unvisited node with the smallest distance;

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
                float alt = dist[u] + u.DistanceTo(v);
                if(alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }

        }

        if(prev[target] == null)
        {
            Debug.Log("Ganas de vivir not found");
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
        selectedUnit.GetComponent<Unit>().currentPath = currentPath;
    }
}
