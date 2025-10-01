using Frogger.Script;
using Godot;
using System;

public partial class Turtle :  Sprite2D , IPlatform 
{
    // Called when the node enters the scene tree for the first time.
    AnimationPlayer animPlayer;
    Area2D area;
	public bool IsdivingTurtle = true;
    public float direction = -1;
    Random rnd = new Random();

    [Export]
    public float velocity = 0.5f;


    public override void _Ready()
	{
        area = GetNode<Area2D>("PlatformArea");
        animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		SetTurtleAnimation();

        velocity = EnemyVelocityManager.SetEnemyVelocity(direction);
        RotationDegrees = EnemyVelocityManager.SetEnemyRotation(direction);


    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
        GlobalPosition = EnemyVelocityManager.MoveWaterEnemy(GlobalPosition,velocity);
        DeSpawner.DeSpawnAsset(this);
    }

	private void SetTurtleAnimation()
	{
        if (IsdivingTurtle)
        {
            animPlayer.Play("Dive");
        }
        else
        {
            animPlayer.Play("Swim");
        }
	}



    public void TurnOffArea2D()
    {
        area.Visible = false;
        area.SetCollisionLayerValue(1, false);
    }

    public void TurnOnArea2D()
    {
        area.Visible = true;
        area.SetCollisionLayerValue(1, true);
    }

    public float GetDirection()
    {
        return direction;
    }
    public float GetVelocity()
    {
        return velocity;
    }

}
