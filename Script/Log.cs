using Godot;
using System;

public partial class Log : Sprite2D
{
    public float Direction = -1;
    private float velocity;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        velocity = 0.5f * Direction;
        if (Direction == -1)
        {
            this.RotationDegrees = 180;
        }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.GlobalPosition = new Vector2(GlobalPosition.X - velocity, GlobalPosition.Y);
	}

}
