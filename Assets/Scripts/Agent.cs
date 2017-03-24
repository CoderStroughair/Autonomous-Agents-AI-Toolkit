using UnityEngine;
using System.Collections;

abstract public class Agent : MonoBehaviour {

    public eLocation location;
    protected bool isMoving;

    abstract public void FixedUpdate();

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

    protected IEnumerator moveAgent(Vector3 newPos)
    {
        //location = eLocation.Moving;
        while (transform.position != newPos)
        {
            yield return null;
            if (transform.position.x != newPos.x)
            {

            }
        }
    }
}