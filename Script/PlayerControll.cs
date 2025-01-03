using Godot;
using System;

public partial class PlayerControll : Sprite2D
{
	Sprite2D player;
	TileMap map;
	AudioStreamPlayer2D frogRibbit;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var path = this.GetPath();
		player = (Sprite2D)GetNode(path);
		map = (TileMap)GetNode("/root/Node2D/TileMap");
		frogRibbit = GetChild<AudioStreamPlayer2D>(0);
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
		else 
		{
			
		}
    }

	private void Move(Vector2 direction)
	{
		Vector2I current_tile = (Vector2I)map.LocalToMap(GlobalPosition);
		Vector2I target_tile = new Vector2I(
			current_tile.X + (int)direction.X,
			current_tile.Y + (int)direction.Y
			);

		GlobalPosition = map.MapToLocal(target_tile);
		frogRibbit.Play();

    }

	private void ChangeFrogTexture(string path)
	{
		player.Texture = (Texture2D)GD.Load("res://Assets/Frog"+path+".png");
    }
}
