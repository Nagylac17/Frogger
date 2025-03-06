using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class DeathHandler : Node
{
    private TileMap map;
    private TileData tileData;
    private static Panel gameOverPanel;
    private static Sprite2D player;
    private static Vector2 playerOriginalPoz;
    public static int lifeLost = 0;

    public DeathHandler(Sprite2D _player,TileMap _tilemap, Panel _gameOver)  
    {
        player = _player;
        map = _tilemap;
        playerOriginalPoz = player.GlobalPosition;
        gameOverPanel = _gameOver; 
    }

    public  void GetCurrentTile(TileData _tileData)
	{
        tileData = _tileData;
    }

    public void GetMap(TileMap _tilemap)
    {
        map = _tilemap;
    }

    public static void PlayerWaterDeathCheck(List<string> area)
    {
        if (area.Any(x => x == "Water") && area.Count == 1 && lifeLost < 1 )
        {
            LifeLoss();
        }

    }

    public static void PlayerCarDeath()
    {
        LifeLoss(); //hozza kell adni a jatekos halal animacio inditasat
    }
    private static void  LifeLoss()
    {
        lifeLost++;
        PositionReset();
        if (GameInterface.GetRemaningLife() == 1)
        {
            gameOverPanel.Visible = true;
        }
        GameInterface.RemoveLifeSprite();
    }

    public static void PositionReset()
    {
        player.GlobalPosition = playerOriginalPoz;
    }

}
