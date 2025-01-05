using Godot;
using System;

 public partial class DeathHandler : Node
{
    private TileMap map;
    private TileData tileData;
    private Sprite2D player;

    public DeathHandler()  
    {
        map = (TileMap)GetNode("/root/Node2D/TileMap");
        player = (Sprite2D)GetNode("/root/Node2D/frog");
        
    }

    public  void GetCurrentTile(TileData _tileData)
	{
        tileData = _tileData;
    }

    public void PlayerDeathCheck()
    {
        if ((bool)tileData.GetCustomData("drowning") == true)
        {
            LifeLoss();
        }
    }
    private void LifeLoss()
    {
        player.QueueFree();
    }

}
