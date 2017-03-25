//This Code was adapted from the tutorial found at http://blog.two-cats.com/2014/06/a-star-example/, which details the basic principles that underline A*.

using System.Collections;
using UnityEngine;

public enum NodeState { Untested, Open, Closed };

public class Node
{
    public Node parent;
    public Vector2 location;
    public NodeState state = NodeState.Untested;
    private eTile tileType;
    public float G;     //Cost to get from the current location to this one
    public float H;     //Cost to get from this node to the final destination
    public float F
    {
        get { return this.G + this.H; }
    }

    public Node() { }
    public Node(Vector2 location, eTile type)
    {
        tileType = type;
        this.location = location;
        this.state = NodeState.Untested;
        this.H = -1;
        this.G = -1;
    }

    public void resetNode(Vector2 endLocation)
    {
        this.parent = null;
        this.state = NodeState.Untested;
        this.H = GetTraversalCost(this.location, endLocation);
        this.G = 0;
    }

    public void AddToChain(ref Node _parent)
    {
        parent = _parent;
        G = parent.G + GetTraversalCost(this.location, parent.location);
    }

    internal static float GetTraversalCost(Vector2 location, Vector2 otherLocation)
    {
        float deltaX = otherLocation.x - location.x;
        float deltaY = otherLocation.y = location.y;
        return (float)Mathf.Sqrt(deltaX * deltaX + deltaY + deltaY);
    }
}
