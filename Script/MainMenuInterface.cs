using Godot;
using System;

public partial class MainMenuInterface : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    private void PlayButtonUp()
	{
        GetTree().ChangeSceneToFile("res://Scenes/Main.tscn");
    }
    private void OnOptionsPressed()
	{
        GetTree().ChangeSceneToFile("res://Scenes/Options.tscn");
    }
    private void OnCreditsPressed()
	{
        
    }
    private void OnExitPressed()
    {
        GetTree().Quit();
    }
}
