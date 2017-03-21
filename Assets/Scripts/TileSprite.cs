using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class TileSprite
{
    //define what attributes a tile has
    // ...
    public string tilename;
    public eTile type;
    public Sprite sprite;
    public SpriteRenderer spriterenderer;
    public TileSprite()
    {
        tilename = "unset";
        type = eTile.Unset;
        sprite = new Sprite();
	}
    public TileSprite(string _name, eTile _type, Sprite image)
    {
        tilename = _name;
        type = _type;
        sprite = image;
    }
}
