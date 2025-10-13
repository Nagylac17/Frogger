using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;

public partial class DeathHandler : Node
{
    private TileMap map;
    private TileData tileData;

    private static Node2D MainNode;
    private static PackedScene gameOverScene;
    private static Panel gameOverPanel;
    private static Sprite2D player;
    private static AnimationPlayer deathAnimations;
    private static Vector2 playerOriginalPoz;
    public static int lifeLossLimit = 0;

    private PackedScene soundManagerScene;
    private static SoundManager soundManager;

    
    public DeathHandler(Sprite2D _player,TileMap _tilemap, Panel _gameOver)  
    {
        player = _player;
        deathAnimations = (AnimationPlayer)player.GetNode("AnimationPlayer");
        map = _tilemap;
        playerOriginalPoz = player.GlobalPosition;
        gameOverScene = GD.Load<PackedScene>("res://Scenes/GameOver.tscn");
        MainNode = (Node2D)_player.GetParent();

        soundManager = (SoundManager)MainNode.GetNode("SoundManager");
;

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
        if (area.Any(x => x == "Water") && area.Count == 1 && lifeLossLimit < 1)
        {

            player.GetTree().CreateTimer(0.3);

            if (area.Any(x => x == "Water") && area.Count == 1 && lifeLossLimit < 1)
            {
                PlayerDrawningDeath();
            }

        }

    }
    public static void PlayerTimeOutDeath()
    {
        soundManager.PlaySound("Main");
        player.GetTree().Paused = true;
        deathAnimations.Play("CarDeath");
    }

    public static void PlayerDrawningDeath()
    {
        soundManager.PlaySound("Water");
        player.GetTree().Paused = true;
        deathAnimations.Play("DrowningDeath");
    }

    public static void PlayerBiteDeath()
    {
        soundManager.PlaySound("Main");
        player.GetTree().Paused = true;
        deathAnimations.Play("CarDeath");
    }

    public static void PlayerCarDeath()
    {
        soundManager.PlaySound("Main");
        player.GetTree().Paused = true;
        deathAnimations.Play("CarDeath"); 
    }
    public static void LifeLoss()
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
