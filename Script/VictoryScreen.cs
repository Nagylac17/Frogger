using Godot;
using System;

public partial class VictoryScreen : Panel
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetPosition();
		GetTree().Paused = true;

    }

	private void SetPosition()
	{
        float x = (GetViewport().GetVisibleRect().Size.X - this.Size.X) / 2;
        float y = (GetViewport().GetVisibleRect().Size.Y - this.Size.Y) / 2;
        this.GlobalPosition = new Vector2(x, y);
    }

}
