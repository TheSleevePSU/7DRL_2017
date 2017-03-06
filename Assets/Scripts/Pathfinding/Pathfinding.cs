using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
    public Transform seeker, target;

    void Update()
    {
        //Debug visualization
        //if (seeker != null && target != null)
        //    FindPath(seeker.position, target.position);
    }

    public void FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        Node startNode = Grid.instance.NodeFromWorldPoint(startPosition);
        Node targetNode = Grid.instance.NodeFromWorldPoint(targetPosition);

        Heap<Node> openSet = new Heap<Node>(Grid.instance.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbor in Grid.instance.GetNeighbors(currentNode))
            {
                if (!neighbor.walkable || closedSet.Contains(neighbor))
                    continue;

                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }

            }
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        Grid.instance.path = path;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int xDistance = Mathf.Abs(Mathf.Abs(nodeA.gridX - nodeB.gridX));
        int yDistance = Mathf.Abs(Mathf.Abs(nodeA.gridY - nodeB.gridY));
        if (Grid.instance.allowDiagonals)
        {
            if (xDistance > yDistance)
                return 14 * yDistance + 10 * (xDistance - yDistance);
            return 14 * xDistance + 10 * (yDistance - xDistance);
        }
        else
        {
            return xDistance + yDistance;
        }
    }
}
