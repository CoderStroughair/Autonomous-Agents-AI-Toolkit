//This Code was adapted from the tutorial found at http://blog.two-cats.com/2014/06/a-star-example/, which details the basic principles that underline A*.

using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum NodeState { Untested, Open, Closed };

public class Node
{
    private Node parentNode;
    public Node parent
    {
        get { return this.parentNode; }
        set
        {
            this.parentNode = value;
            this.G = this.parentNode.G + GetTraversalCost(this.location, this.parentNode.location);
        }
    }
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
    public Node(Vector2 location, eTile type, Vector2 endLocation)
    {
        tileType = type;
        this.location = location;
        this.state = NodeState.Untested;
        this.H = GetTraversalCost(this.location, endLocation);
        this.G = 0;
    }

    public void resetNode(Vector2 endLocation)
    {
        this.state = NodeState.Untested;
        this.H = GetTraversalCost(this.location, endLocation);
        this.G = 0;
    }

    internal static float GetTraversalCost(Vector2 location, Vector2 otherLocation)
    {
        float deltaX = otherLocation.x - location.x;
        float deltaY = otherLocation.y - location.y;
        return (float)Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }
}
