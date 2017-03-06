using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public bool debugDrawGizmos;
    public bool allowDiagonals;
    private int gridSizeX;
    private int gridSizeY;
    //public float nodeRadius;
    Node[,] grid;

    public static Grid instance;
    public Pathfinding pathfinding;

    void Start()
    {
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y);
        CreateGrid();
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }

    public int MaxSize
    {
        get { return gridSizeX * gridSizeY; }
    }

    public void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Collider2D[] c2d = Physics2D.OverlapPointAll(new Vector2(x, y));
                bool walkable = false;
                foreach (Collider2D c in c2d)
                {
                    Tile t = c.gameObject.GetComponent<Tile>();
                    if (t != null)
                        if (t.isWalkable)
                            walkable = true;
                }
                grid[x, y] = new Node(walkable, new Vector3(x, y, 0), x, y);
            }
        }
    }

    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        if (allowDiagonals)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;

                    if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    {
                        neighbors.Add(grid[checkX, checkY]);
                    }
                }
            }
        }
        else
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;
                    if (Mathf.Abs(x) == 1 && Mathf.Abs(y) == 1)
                        continue;

                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;

                    if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    {
                        neighbors.Add(grid[checkX, checkY]);
                    }
                }
            }
        }
        return neighbors;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        int x = Mathf.RoundToInt(worldPosition.x);
        int y = Mathf.RoundToInt(worldPosition.y);
        return grid[x, y];
    }

    public List<Node> path;

    void OnDrawGizmos()
    {
        if (debugDrawGizmos)
        {
            Vector3 centerOfGrid = transform.position + (Vector3.right * gridSizeX / 2) + (Vector3.up * gridSizeY / 2);
            Gizmos.DrawWireCube(centerOfGrid, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

            if (grid != null)
            {
                foreach (Node n in grid)
                {
                    Gizmos.color = n.walkable ? Color.white : Color.red;
                    if (path != null)
                        if (path.Contains(n))
                            Gizmos.color = Color.black;
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * 0.5f);
                }
            }
        }
    }
}

