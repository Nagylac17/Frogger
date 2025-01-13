using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class DeathHandler : Node
{
    private TileMap map;
    private TileData tileData;    
    private Sprite2D player;
    private Vector2 playerOriginalPoz;

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
    public void GetPlayer(Sprite2D _player)
    {
        
    }

    public void GetMap(TileMap _tilemap)
    {
        map = _tilemap;
    }

    public void PlayerDeathCheck(List<string> area)
    {
        if (area.Any(x => x != "Water") || area.Count == 0)
        {
        }
        else
        {
            LifeLoss();
        }
    }
    private void LifeLoss()
    {
        player.GlobalPosition = playerOriginalPoz;
    }

}
