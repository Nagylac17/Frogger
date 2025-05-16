using Frogger.Script;
using Godot;
using System;

public partial class Aligator : Sprite2D, IPlatform
{
    // Called when the node enters the scene tree for the first time.
    AnimationPlayer animPlayer;
    Area2D biteArea;
    public bool IsAligatorBite = false;
    public float Direction = -1;
    Random rnd = new Random();

    [Export]
    public float velocity = 0.5f;

    public override void _Ready()
	{
        biteArea = GetNode<Area2D>("BiteArea");
        animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        SetAligatorAnimation();

        velocity = (velocity + Victory.VelocityIncrease()) * Direction;
        if (Direction == -1)
        {
            RotationDegrees = 180;
            FlipV = true;
        }


    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
        GlobalPosition = new Vector2(GlobalPosition.X - velocity, GlobalPosition.Y);
        DeSpawner.DeSpawnAsset(this);
    }

    private void SetAligatorAnimation()
    {
        if (rnd.Next( 1 , 3 ) == 1)
        {
            animPlayer.Play("FriendlySwim");
            TurnOffBiteArea2D();

        }
        else
        {
            animPlayer.Play("DangerousSwim");
        }
    }

    public void TurnOffBiteArea2D()
    {
        biteArea.Visible = false;
        biteArea.SetCollisionLayerValue(1, false);
        biteArea.SetCollisionMaskValue(2, false);
    }

    public void TurnOnBiteArea2D()
    {
        biteArea.Visible = true;
        biteArea.SetCollisionLayerValue(1, true);
        biteArea.SetCollisionMaskValue(2, true);
    }

    public float GetDirection()
    {
        return Direction;
    }
    public float GetVelocity()
    {
        return velocity;
    }

    void OnBiteAreaEntered(Area2D area)
    {
        if (area.Name == "Frog")
        {
            DeathHandler.PlayerCarDeath();
        }
    }
}
