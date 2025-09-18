using Frogger.Script;
using Godot;
using System;
using System.Collections.Generic;
using static Godot.TextServer;

public partial class Log : Sprite2D, IPlatform
{
    public float direction = -1;
    public float velocity;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        velocity = EnemyVelocityManager.SetEnemyVelocity(direction);
        RotationDegrees = EnemyVelocityManager.SetEnemyRotation(direction);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
        GlobalPosition = EnemyVelocityManager.MoveEnemy(GlobalPosition, velocity);
        DeSpawner.DeSpawnAsset(this);
    }

    public float GetVelocity()
    {
        return velocity;
    }
    public float GetDirection()
    {
        return direction;
    }

}
