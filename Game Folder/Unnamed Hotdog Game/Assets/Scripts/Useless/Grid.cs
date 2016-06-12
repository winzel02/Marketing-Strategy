using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
	
	public LayerMask unPlaceableMask;
	public Vector2 gridSize;
	public float nodeRadius = 1;
	Node[,] grid;
	float nodeDiameter;
	int gridSizeX, gridSizeY;
	void Start()
	{
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt (gridSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt (gridSize.y / nodeDiameter);
		CreateGrid ();
	}
	void CreateGrid()
	{
		grid = new Node[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.forward * gridSize.y / 2;
		for(int x = 0; x < gridSizeX; x ++)
		{
			for(int y = 0; y < gridSizeY; y++)
			{
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool placeable = !(Physics.CheckSphere (worldPoint, nodeRadius, unPlaceableMask));
				grid [x, y] = new Node (placeable, worldPoint);
			}
		}
	}
	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 0.5f, gridSize.y));
		if (grid != null) {
			foreach (Node n in grid) {
				Gizmos.color = (n.placeable) ? Color.white : Color.red;
				Gizmos.DrawCube (n.worldPosition, Vector3.one * (nodeDiameter - .01f));
			}
		}
	}
}

public class Node
{
	public bool placeable;
	public Vector3 worldPosition;

	public Node(bool _placeable, Vector3 _worldPos)
	{
		placeable = _placeable;
		worldPosition = _worldPos;
	}
}
