using Frogger.Script;
using Godot;
using System;
using static Godot.TextServer;

public partial class Car : AnimatedSprite2D
{
    public float direction = -1;
    public float velocity;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        velocity = EnemyVelocityManager.SetEnemyVelocity(direction);
        RotationDegrees = EnemyVelocityManager.SetEnemyRotation(direction);
        this.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (velocity < EnemyVelocityManager.SetEnemyVelocity(direction))
        {
            velocity = EnemyVelocityManager.SetEnemyVelocity(direction);
        }

        GlobalPosition = EnemyVelocityManager.MoveEnemy(GlobalPosition, velocity);
        DeSpawner.DeSpawnAsset(this);
    }

    public void AreaEntered(Area2D area)
    {
        if (area.Name == "Frog")
        {
             DeathHandler.PlayerCarDeath();   
        }
    }
}
