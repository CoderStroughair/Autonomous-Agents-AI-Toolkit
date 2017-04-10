using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Lean; //from Unity asset "LeanPool" - freely available in the Asset Store; used here for object pooling

public class GameController : MonoBehaviour {

	public List<TileSprite> TileSprites;
	public Vector2 MapSize;
	public Sprite DefaultImage;
	public GameObject TileContainerPrefab;
	public GameObject TilePrefab;
	public Vector2 CurrentPosition;

    public TileSprite[,] _map;
    private GameObject _tileContainer;
    public Solver pathfinder;
	private List<GameObject> _tiles = new List<GameObject>();
    public bool characterMovement = true;
    private int framesPassed = 0;

	//create a map of size MapSize of unset tiles
	private void DefaultTiles()
    {
        for (var y = 0; y < MapSize.y - 1; y++)
        {
            for (var x = 0; x < MapSize.x - 1; x++)
            {
                _map[x, y] = new TileSprite("unset", eTile.Unset, DefaultImage);
            }
        }
    }

	//set the tiles of the map to what is specified in TileSprites
	private void SetTiles()
    {
        int numSpecTiles = 5;
        Vector2[] specialLocations = new Vector2[numSpecTiles];
        for (int i = 0; i < numSpecTiles; i++)
        {
            specialLocations[i] = new Vector2(UnityEngine.Random.Range(0, (int)MapSize.x-2), UnityEngine.Random.Range(0, (int)MapSize.y-2));
            for (int j = 0; j < i; j++)
            {
                if (specialLocations[j] == specialLocations[i])
                {
                    specialLocations[i] = new Vector2(UnityEngine.Random.Range(0, (int)MapSize.x-2), UnityEngine.Random.Range(0, (int)MapSize.y-2));
                    j = 0;
                }
            }
        }

        var index = 0;
        for (var y = 0; y < MapSize.y; y++)
        {
            for (var x = 0; x < MapSize.x; x++)
            {
                var random = UnityEngine.Random.Range(0.0f, 1.0f);
                if (random < 0.8)
                    index = 1;
                else
                    index = 2;
                _map[x, y] = new TileSprite(TileSprites[index].tilename, TileSprites[index].type, TileSprites[index].sprite);

                Vector2 currLoc = new Vector2(x, y);
                for (int i = 0; i < numSpecTiles; i++)
                {
                    if (currLoc == specialLocations[i])
                    {
                        _map[x, y] = new TileSprite(TileSprites[i+3].tilename, TileSprites[i+3].type, TileSprites[i+3].sprite);
                        break;
                    }
                }
               
            }
        }
    }

	private void AddTilesToMap()
    {
        foreach (GameObject o in _tiles)
        {
            LeanPool.Despawn(o);
        }
        _tiles.Clear();
        LeanPool.Despawn(_tileContainer);
        _tileContainer = LeanPool.Spawn(TileContainerPrefab);
        var tileSize = 1f;
        var viewOffsetX = MapSize.x / 2f;
        var viewOffsetY = MapSize.y / 2f;
        for (int y = 0; y < MapSize.y; y++)
        {
            for (int x = 0; x < MapSize.x; x++)
            {
                var tX = (x - viewOffsetX + 0.5f) * tileSize;
                var tY = (y - viewOffsetY + 0.5f) * tileSize;

                var iX = x + CurrentPosition.x;
                var iY = y + CurrentPosition.y;

                if (iX < 0) continue;
                if (iY < 0) continue;
                if (y > MapSize.x) continue;
                if (y > MapSize.y) continue;

                var t = LeanPool.Spawn(TilePrefab);
                t.transform.position = new Vector3(tX, tY, 0);
                t.transform.SetParent(_tileContainer.transform);
                var renderer = t.GetComponent<SpriteRenderer>();
                renderer.sprite = _map[(int)x, (int)y].sprite;
                if (_map[(int)x, (int)y].shaded)
                {
                    renderer.color = Color.gray;
                }
                else
                    renderer.color = Color.white;

                _tiles.Add(t);
            }
        }
    }

    public void Start()
    {
        _map = new TileSprite[(int)MapSize.x, (int)MapSize.y];
        DefaultTiles();
        SetTiles();
        AddTilesToMap();
        pathfinder = new Solver(_map);
    }

    private void FixedUpdate()
    {
        characterMovement = false;
        if (framesPassed == 0)
            characterMovement = true;

        framesPassed ++;
        if (framesPassed > 40)
            framesPassed = 0;

        AddTilesToMap();
    }

    //Direction is the direction in which the agent is looking. If its set to 0, it means that the agent can see in all directions. 
    public List<Agent> AgentSee(Agent a, int maxDistance, Vector3 direction, ref List<Vector2> locations)
    {
        List<Agent> agentsOut = new List<Agent>();
        GameObject[] agents = GameObject.FindGameObjectsWithTag("Agent");
        Vector2 agentLocation = a.GetGridPosition();
        foreach (GameObject agent in agents)
        {
            Agent target = agent.GetComponent<Agent>();
            if (target == a)
                continue;
            Vector2 targetLocation = target.GetGridPosition();
            if(direction == new Vector3(0.0f, 0.0f, 0.0f))
            {
                if(Vector2.Distance(agentLocation, targetLocation) <= maxDistance)
                {
                    locations.Add(targetLocation);
                    agentsOut.Add(target);
                }
            }
            else
            {
                Vector3 distance = targetLocation - agentLocation;
                distance = Vector3.ClampMagnitude(distance, 1.0f);
                direction = Vector3.ClampMagnitude(direction, 1.0f);
                if((Vector2.Distance(agentLocation, targetLocation) <= maxDistance) && distance == direction)
                {
                    locations.Add(targetLocation);
                    agentsOut.Add(target);
                }
            }
        }
        return agentsOut;
    }

    public List<Agent> AgentHear(Agent a, int maxDistance, ref List<Vector2> locations)
    {
        List<Agent> agentsOut = new List<Agent>();
        GameObject[] agents = GameObject.FindGameObjectsWithTag("Agent");
        Vector2 agentLocation = a.GetGridPosition();
        foreach(GameObject agent in agents)
        {
            Agent target = agent.GetComponent<Agent>();
            if (target == a)
                continue;
            Vector2 targetLocation = target.GetGridPosition();
            if(pathfinder.getPathLength(agentLocation, targetLocation) < maxDistance)
                agentsOut.Add(target);
        }
        return agentsOut;
    }
}
