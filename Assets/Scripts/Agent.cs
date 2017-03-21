using UnityEngine;
using System.Collections;

abstract public class Agent : MonoBehaviour {

    public eLocation location;

    abstract public void Update ();

    protected Vector2 getPosition()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        TilingEngine controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<TilingEngine>();
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
                    return new Vector2(i - cameraPosition.x, j - cameraPosition.y);
            }
        }


        return new Vector2(-1, -1);
    }
}