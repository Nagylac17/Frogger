using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class PlayerControll : Sprite2D
{
	Sprite2D player;
	TileMap map;
	AudioStreamPlayer2D frogRibbit;
	DeathHandler deathHandler;
	public List<string> ActiveMonitoredAreas = new List<string>();
	Node OriginalParent;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var path = this.GetPath();
		player = (Sprite2D)GetNode(path);
		map = (TileMap)GetNode("/root/Node2D/TileMap");
		frogRibbit = GetChild<AudioStreamPlayer2D>(0);
		OriginalParent = GetParent();
		
        deathHandler = new DeathHandler(player, map);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

        if (Input.IsActionJustPressed("up"))
		{
			//player.GlobalPosition = new Vector2(player.GlobalPosition.X, player.GlobalPosition.Y - 1);
			ChangeFrogTexture("Up");
			Move(Vector2.Up);

		}
		else if (Input.IsActionJustPressed("down"))
		{
			//player.GlobalPosition = new Vector2(player.GlobalPosition.X, player.GlobalPosition.Y + 1);
			ChangeFrogTexture("Down");
			Move(Vector2.Down);
		}
		else if (Input.IsActionJustPressed("left"))
		{
			//player.GlobalPosition = new Vector2(player.GlobalPosition.X - 1, player.GlobalPosition.Y);
			ChangeFrogTexture("Left");
			Move(Vector2.Left);
		}
		else if (Input.IsActionJustPressed("right"))
		{
			//player.GlobalPosition = new Vector2(player.GlobalPosition.X + 1, player.GlobalPosition.Y);
			ChangeFrogTexture("Right");
			Move(Vector2.Right);
		}
        deathHandler.PlayerDeathCheck(ActiveMonitoredAreas);
    }

	Vector2I TargetTile(Vector2 direction, Vector2I current_tile)
	{
		Vector2I targetTile = new Vector2I(
            current_tile.X + (int)direction.X,
            current_tile.Y + (int)direction.Y
            );

        return targetTile;
	}

	private void Move(Vector2 direction)
	{
		GD.Print(this.OriginalParent);
		Vector2I current_tile = (Vector2I)map.LocalToMap(GlobalPosition);
		Vector2I target_tile = TargetTile(direction, current_tile);
		var tile_data = map.GetCellTileData(0, target_tile);
		
		if (tile_data == null)
		{ return;}

        deathHandler.GetCurrentTile(tile_data);

		//if ((bool)tile_data.GetCustomData("walkable") == false && (bool)tile_data.GetCustomData("drowning") == false && walkablePath == false)
		//{return;}

		GlobalPosition = map.MapToLocal(target_tile);
		frogRibbit.Play();
		//deathHandler.PlayerDeathCheck(areaTest);		
    }
	public void Area_entered(Area2D area)
	{
		ActiveMonitoredAreas.Add(area.Name);
    }
    public void Area_Exited(Area2D area)
	{
		ActiveMonitoredAreas.Remove(area.Name); 
    }

    private void ChangeFrogTexture(string path)
	{
		player.Texture = (Texture2D)GD.Load("res://Assets/Frog"+path+".png");
    }
}
