using Godot;
using System;

public partial class Main : Node2D
{
    Node2D pauseMenu;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        pauseMenu = (Node2D)GetNode("/root/Node2D/PauseMenu");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

        if (Input.IsActionJustPressed("pause"))
        {
            pauseMenu.Visible = !pauseMenu.Visible;
            GetTree().Paused = pauseMenu.Visible;
        }
    }
}
