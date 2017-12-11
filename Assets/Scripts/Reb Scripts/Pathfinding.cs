using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
	public int tileX, tileY;
	public AdaptedMap map;
	int moveSpeed = 8;
	float remainingMovement = 2;
	public List<Node> currentPath = null;

	void Awake(){
		map = GameObject.Find("New Map").GetComponent<AdaptedMap> ();
	}

	void Update()
	{
		if (currentPath != null)
		{
			int currNode = 0;
			while (currNode < currentPath.Count - 1)
			{
				Vector3 start = map.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].y) + new Vector3(0, 1, 0);
				Vector3 end = map.TileCoordToWorldCoord(currentPath[currNode + 1].x, currentPath[currNode + 1].y) + new Vector3(0, 1, 0);

				Debug.DrawLine(start, end, Color.red);

				currNode++;
			}
		}

		if (Vector3.Distance(transform.position, map.TileCoordToWorldCoord(tileX, tileY) + transform.up * 0.5f) < 0.1f)
			AdvancePathing();
		Vector3 nextPosition = map.TileCoordToWorldCoord (tileX, tileY) + transform.up * 0.5f;
		transform.position = Vector3.Lerp(transform.position, nextPosition, 5f * Time.deltaTime);
	}

	void AdvancePathing()
	{
		if (currentPath == null)
			return;

		if (remainingMovement <= 0)
			return;

		transform.position = map.TileCoordToWorldCoord(tileX, tileY) + transform.up * 0.5f;

		remainingMovement -= map.CostToEnterTile(currentPath[0].x, currentPath[0].y, currentPath[1].x, currentPath[1].y);

		tileX = currentPath[1].x;
		tileY = currentPath[1].y;

		currentPath.RemoveAt(0);

		if (currentPath.Count == 1)
		{
			currentPath = null;
		}
	}

	public void NextTurn()
	{
		while (currentPath != null && remainingMovement > 0)
		{
			AdvancePathing();
		}

		remainingMovement = moveSpeed;
	}

	public void TileAction(GameObject hitObject)
	{
		if (Input.GetMouseButton(0))
		{
			int x = hitObject.GetComponentInParent<Hex>().tileX;
			int y = hitObject.GetComponentInParent<Hex>().tileY;

			map.GeneratePathTo(x, y);
		}
	}

	public void SetSelected(GameObject obj){
		map.selectedUnit = obj;
	}
}



