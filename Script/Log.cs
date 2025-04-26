using Frogger.Script;
using Godot;
using System;

public partial class Log : Sprite2D, IPlatform
{
    public float Direction = -1;
    public float velocity;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        velocity = (0.5f+Victory.VelocityIncrease()) * Direction;
        if (Direction == -1)
        {
            RotationDegrees = 180;
        }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GlobalPosition = new Vector2(GlobalPosition.X - velocity, GlobalPosition.Y);

       DeSpawner.DeSpawnAsset(this);
    }

    public float GetVelocity()
    {
        return velocity;
    }
    public float GetDirection()
    {
        return Direction;
    }

    //private void Despawn()
    //{
    //    if(GlobalPosition.X < -200 || GlobalPosition.X > 750)
    //    {
    //        this.QueueFree();
    //    }
    //}

}
