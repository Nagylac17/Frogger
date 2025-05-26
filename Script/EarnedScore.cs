using Godot;
using System;

public partial class EarnedScore : Label
{
	Vector2 originalPos;
	Timer timer;
	float velocity = 70;
	[Export]
	public string score = "100";


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{

		Text = score;
		originalPos = GlobalPosition;
		timer = GetNode<Timer>("DeleteTimer");
		timer.Start();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GlobalPosition =  new Vector2 (GlobalPosition.X,GlobalPosition.Y - (float)(velocity * delta));
	}

	private void DeleteSelf()
	{
		QueueFree();
	}
	public void OnDeleteTimerTimeout()
	{
		DeleteSelf();

    }
}
