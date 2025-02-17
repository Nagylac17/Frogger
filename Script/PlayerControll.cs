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
	private float direction;//Ez itt lehet publicra kell
    private float velocity; //Ez itt lehet publicra kell
	private bool IsOnePlatform = false;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var path = this.GetPath();
		player = (Sprite2D)GetNode(path);
		map = (TileMap)GetNode("/root/Node2D/TileMap");
		frogRibbit = GetChild<AudioStreamPlayer2D>(0);
		deathHandler = new DeathHandler(player, map);
        velocity = 0;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if (Input.IsActionJustPressed("up"))
		{
			player.RotationDegrees = 360;
			Move(Vector2.Up);
			GameInterface.AddScore(10);

		}
		else if (Input.IsActionJustPressed("down"))
		{
			player.RotationDegrees = 180;
			Move(Vector2.Down);
			GameInterface.Subtract(10);
		}
		else if (Input.IsActionJustPressed("left"))
		{
			player.RotationDegrees = -90;
			Move(Vector2.Left);
		}
		else if (Input.IsActionJustPressed("right"))
		{
			player.RotationDegrees = 90;
			Move(Vector2.Right);
		}


		if (IsOnePlatform && (GlobalPosition.X < 630 && GlobalPosition.X > 5 )) // tilemap two end
		{
			this.GlobalPosition = new Vector2(GlobalPosition.X - velocity , GlobalPosition.Y);
		}

       // DeathHandler.PlayerWaterDeathCheck(ActiveMonitoredAreas);


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
		Vector2I current_tile = (Vector2I)map.LocalToMap(GlobalPosition);
		Vector2I target_tile = TargetTile(direction, current_tile);
		var tile_data = map.GetCellTileData(0, target_tile);

        if (tile_data == null || (bool)tile_data.GetCustomData("walkable") == false)
			{ return; }


		deathHandler.GetCurrentTile(tile_data);
		DeathHandler.lifeLossLimit = 0;

        //if ((bool)tile_data.GetCustomData("walkable") == false && (bool)tile_data.GetCustomData("drowning") == false && walkablePath == false)
        //{return;}
        GlobalPosition = map.MapToLocal(target_tile);
		frogRibbit.Play();
		//deathHandler.PlayerDeathCheck(areaTest);		
	}
	public void Area_entered(Area2D area)
	{
		if (area.Name == "Log") //interface-el egy platform osszegzest
		{ 
			SetFrogVelocity(area.GetParent());
			SetFrogDirection(area.GetParent());
			IsOnePlatform = true;
		}
		ActiveMonitoredAreas.Add(area.Name);
    }
	public void Area_Exited(Area2D area)
	{
		velocity = 0;
		direction = 0;
		IsOnePlatform = false;
		ActiveMonitoredAreas.Remove(area.Name);

	}

	private void ChangeFrogTexture(string path)
	{
		player.Texture = (Texture2D)GD.Load("res://Assets/Frog" + path + ".png");
	}

	private void SetFrogVelocity(Node node)
	{
			Log Log = (Log)node;
			this.velocity = Log.velocity;	
	}

	private void SetFrogDirection(Node node)
	{
			Log Log = (Log)node;
			this.direction = Log.Direction;
	}
}
