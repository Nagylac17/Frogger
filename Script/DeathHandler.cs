using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class DeathHandler : Node
{
    private TileMap map;
    private TileData tileData;    
    private static Sprite2D player;
    private static Vector2 playerOriginalPoz;

    public DeathHandler(Sprite2D _player,TileMap _tilemap)  
    {
        player = _player;
        map = _tilemap;
        playerOriginalPoz = player.GlobalPosition;
    }

    public  void GetCurrentTile(TileData _tileData)
	{
        tileData = _tileData;
    }

    public void GetMap(TileMap _tilemap)
    {
        map = _tilemap;
    }

    public void PlayerWaterDeathCheck(List<string> area)
    {
        if (area.Any(x => x != "Water") || area.Count == 0)
        {
        }
        else
        {
            LifeLoss();
            PositionReset();
           
        }
    }
    private void LifeLoss()
    {
        player.GlobalPosition = playerOriginalPoz;
        GameInterface.RemoveLifeSprite();
    }

    public static void PositionReset()
    {
        player.GlobalPosition = playerOriginalPoz;
    }

}
