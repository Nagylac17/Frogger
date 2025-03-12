using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class DeathHandler : Node
{
    private TileMap map;
    private TileData tileData;

    private static Node2D MainNode;
    private static PackedScene gameOverScene;
    private static Panel gameOverPanel;
    private static Sprite2D player;
    private static Vector2 playerOriginalPoz;
    public static int lifeLossLimit = 0;

    public DeathHandler(Sprite2D _player,TileMap _tilemap, Panel _gameOver)  
    {
        player = _player;
        map = _tilemap;
        playerOriginalPoz = player.GlobalPosition;
       // gameOverPanel = _gameOver;
        gameOverScene = GD.Load<PackedScene>("res://Scenes/GameOver.tscn");
        MainNode = (Node2D)_player.GetParent();
        //pauseMenu = (Node2D)GetNode("/root/Node2D/PauseMenu");

    }

    public void GetCurrentTile(TileData _tileData)
	{
        tileData = _tileData;
    }

    public void GetMap(TileMap _tilemap)
    {
        map = _tilemap;
    }

    public static void PlayerWaterDeathCheck(List<string> area)
    {
        if (area.Any(x => x == "Water") && area.Count == 1 && lifeLossLimit < 1 )
        {
           
             player.GetTree().CreateTimer(0.3);
            
            if (area.Any(x => x == "Water") && area.Count == 1 && lifeLossLimit < 1)
            {
                 LifeLoss();
                   // GD.Print("Hiba!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
                
        }

    }

    public static void PlayerCarDeath()
    {
         LifeLoss(); //hozza kell adni a jatekos halal animacio inditasat
    }
    private static void LifeLoss()
    {
        lifeLossLimit++;
        PositionReset();
        if (GameInterface.GetRemaningLife() == 1)
        {
            setGameOverPanel();
            MainNode.AddChild(gameOverPanel);
        }
        GameInterface.RemoveLifeSprite();
    }

    public static void PositionReset()
    {
        player.GlobalPosition = playerOriginalPoz;
    }

    private static void setGameOverPanel()
    {
        gameOverPanel = (Panel)gameOverScene.Instantiate();
    }

}
