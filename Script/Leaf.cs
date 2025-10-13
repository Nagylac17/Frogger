using Godot;
using System;

public partial class Leaf : Sprite2D
{
	Sprite2D Wfrog = new Sprite2D();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Wfrog = GetChild<Sprite2D>(1);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void AreaEntered (Area2D area)
	{
		if (Wfrog.Visible)
		{
            DeathHandler.PlayerCarDeath();
        }
		else
		{
			Wfrog.Visible = true;
			Victory.AddWFrog(this);
            DeathHandler.PositionReset();
		}
	}

	public void HideWFrog()
	{
        Wfrog.Visible = false;
	}
}
