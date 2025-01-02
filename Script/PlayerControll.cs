using Godot;
using System;

public partial class PlayerControll : Sprite2D
{
	Sprite2D player;
	TileMap map;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var path = this.GetPath();
		player = (Sprite2D)GetNode(path);
		map = (TileMap)GetNode("/root/Node2D/TileMap");
		testing();

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("up"))
		{
			//player.GlobalPosition = new Vector2(player.GlobalPosition.X, player.GlobalPosition.Y - 1);
			Move(Vector2.Up);

        }		
		else if (Input.IsActionJustPressed("down"))
        {
            //player.GlobalPosition = new Vector2(player.GlobalPosition.X, player.GlobalPosition.Y + 1);
            Move(Vector2.Down);
        }
        else if (Input.IsActionJustPressed("left"))
        {
            //player.GlobalPosition = new Vector2(player.GlobalPosition.X - 1, player.GlobalPosition.Y);
            Move(Vector2.Left);
        }
        else if (Input.IsActionJustPressed("right"))
        {
            //player.GlobalPosition = new Vector2(player.GlobalPosition.X + 1, player.GlobalPosition.Y);
            Move(Vector2.Right);
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
	}

	private void testing()
	{
		var ehe = GlobalPosition;
		var kkk = map.LocalToMap(player.GlobalPosition);
		
	}
}
