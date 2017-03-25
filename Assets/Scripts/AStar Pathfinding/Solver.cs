using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Solver
{
    private Node[,] map;
    private TileSprite[,] tileMap;
    private Node end;
    static Solver(){}
    private Solver() { }

    public Solver(TileSprite[,] tileMap)
    {
        this.tileMap = tileMap;
        int width = tileMap.GetLength(0);
        int height = tileMap.GetLength(1);
        Debug.Log(width + "," + height);
        this.map = new Node[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                this.map[x, y] = new Node(new Vector2(x, y), tileMap[x, y].type);
            }
        }
    }

    public Queue<Node> solve(Vector2 startLocation, Vector2 endLocation)
    {
        end = new Node(endLocation, eTile.Unset);
        int width = tileMap.GetLength(0);
        int height = tileMap.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                this.map[x, y].resetNode(endLocation);
            }
        }

        Queue<Node> queue = new Queue<Node>();

        bool completed = search(map[(int)startLocation.x, (int)startLocation.y]);

        if (completed)
        {
            Node curr = map[(int)endLocation.x, (int)endLocation.y];
            while (curr.parent != null)
            {
                Debug.Log(curr.location.x + "," + curr.location.y);
                queue.Enqueue(curr);
                curr = curr.parent;
            }
            queue.Reverse();
        }

        return queue;
    }

    private bool search(Node curr)
    {
        curr.state = NodeState.Closed;
        List<Node> adjacentNodes = GetAdjacentNodes(curr);

        adjacentNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));
        foreach (Node adjacentNode in adjacentNodes)
        {
            // Check whether the end node has been reached
            if (adjacentNode.location == this.end.location)
            {
                Debug.Log("Finished Pathing");
                end = adjacentNode;
                return true;
            }
            else
            {
                // If not, check the next set of nodes
                if (search(adjacentNode)) // Note: Recurses back into Search(Node)
                    return true;
            }
        }
        return false;
    }

    private List<Node> GetAdjacentNodes(Node fromNode)
    {
        List<Node> walkableNodes = new List<Node>();
        IEnumerable<Vector2> nextLocations = GetAdjacentLocations(fromNode.location);

        foreach (Vector2 location in nextLocations)
        {
            int x = (int)location.x;
            int y = (int)location.y;


            // Stay within the grid's boundaries
            if ((x < 0) || (x >= map.GetLength(0)) || (y < 0) || (y >= map.GetLength(1)))
                continue;

            Node curr = map[x, y];

            // Ignore already-closed nodes
            if (curr.state == NodeState.Closed)
                continue;

            // Already-open nodes are only added to the list if their G-value is lower going via this route.
            if (curr.state == NodeState.Open)
            {
                float traversalCost = Node.GetTraversalCost(curr.location, curr.parent.location);
                float gTemp = fromNode.G + traversalCost;
                if (gTemp < curr.G)
                {
                    if (curr != fromNode)
                        curr.parent = fromNode;
                    walkableNodes.Add(curr);
                }
            }
            else
            {
                // If it's untested, set the parent and flag it as 'Open' for consideration
                if (curr != fromNode)
                    curr.parent = fromNode;
                curr.state = NodeState.Open;
                walkableNodes.Add(curr);
            }
        }

        return walkableNodes;
    }

    private IEnumerable<Vector2> GetAdjacentLocations(Vector2 fromLocation)
    {
        List<Vector2> result = new List<Vector2>{
                new Vector2(fromLocation.x-1, fromLocation.y-1),
                new Vector2(fromLocation.x-1, fromLocation.y  ),
                new Vector2(fromLocation.x-1, fromLocation.y+1),
                new Vector2(fromLocation.x,   fromLocation.y+1),
                new Vector2(fromLocation.x+1, fromLocation.y+1),
                new Vector2(fromLocation.x+1, fromLocation.y  ),
                new Vector2(fromLocation.x+1, fromLocation.y-1),
                new Vector2(fromLocation.x,   fromLocation.y-1)
            };

        for (int i = 0; i < result.Count; i++)
            if (result[i].x >= map.GetLength(0) || result[i].y >= map.GetLength(1))
                result.RemoveAt(i);
        return result;
    }
}
