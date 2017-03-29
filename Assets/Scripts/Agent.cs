using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract public class Agent : MonoBehaviour {

    public eLocation location;
    public eLocation destination = eLocation.UNSET;
    public bool isMoving;
    public bool dead = false;
    protected GameController controller;
    public Stack<Node> path = new Stack<Node>();
    private bool first = true;

    abstract public void FixedUpdate();

    public bool doMovement()    //will return true only if the character reaches their destination, or isn't moving
    {
        if (first)
        {
            first = false;
            transform.position = GetGlobalPosition();
            destination = location;
            return true;
        }
        if (dead || !controller.characterMovement)
            return false;
        if (location == destination)
            return true;

        if (path.Count == 0)
        {
            path = controller.pathfinder.solve(GetGridPosition(location), GetGridPosition(destination));
        }
        if (path.Count == 0)
            Debug.Log("Crashing...");

        Node nextSquare = path.Pop();
        Vector2 MapSize = controller.MapSize;
        var viewOffsetX = MapSize.x / 2f;
        var viewOffsetY = MapSize.y / 2f;
        var tX = (nextSquare.location.x - viewOffsetX + 0.5f) * 1f;
        var tY = (nextSquare.location.y - viewOffsetY + 0.5f) * 1f;
        transform.position = new Vector2(tX, tY);
        if(path.Count == 0)
        {
            location = destination;
            isMoving = false;
            return true;
        }
        return false;
    }

    protected Vector2 GetGlobalPosition()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        TileSprite[,] map = controller._map;
        eTile mapLoc = eTile.NUM_TILES;
        switch (location)
        {
            case eLocation.Bank:
            case eLocation.SheriffsOffice:
            case eLocation.Undertakers:
                mapLoc = eTile.Town;
                break;
            case eLocation.Shack:
                mapLoc = eTile.Shack;
                break;
            case eLocation.Mine:
                mapLoc = eTile.Mine;
                break;
            case eLocation.OutlawCamp:
                mapLoc = eTile.Encampment;
                break;
            case eLocation.Cemetery:
                mapLoc = eTile.Cemetary;
                break;
        }
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j].type == mapLoc)
                {
                    Vector2 MapSize = controller.MapSize;
                    var viewOffsetX = MapSize.x / 2f;
                    var viewOffsetY = MapSize.y / 2f;
                    var tX = (i - viewOffsetX + 0.5f) * 1f;
                    var tY = (j - viewOffsetY + 0.5f) * 1f;
                    return new Vector2(tX, tY);
                }
            }
        }
        return new Vector2(-1, -1);
    }

    protected Vector2 GetGridPosition(eLocation currPos)
    {
        TileSprite[,] map = controller._map;
        eTile mapLoc = eTile.NUM_TILES;
        switch (currPos)
        {
            case eLocation.Bank:
            case eLocation.SheriffsOffice:
            case eLocation.Undertakers:
                mapLoc = eTile.Town;
                break;
            case eLocation.Shack:
                mapLoc = eTile.Shack;
                break;
            case eLocation.Mine:
                mapLoc = eTile.Mine;
                break;
            case eLocation.OutlawCamp:
                mapLoc = eTile.Encampment;
                break;
            case eLocation.Cemetery:
                mapLoc = eTile.Cemetary;
                break;
        }

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j].type == mapLoc)
                {
                    return new Vector2(i, j);
                }
            }
        }

        return new Vector2(-1, -1);
    }
}