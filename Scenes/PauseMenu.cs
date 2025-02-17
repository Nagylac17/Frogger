using Godot;
using System;

public partial class PauseMenu : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

    }

	private void OnResumePressed()
	{
		resume();
    }
	private void OnOptionsPressed()
	{
		
    }
	private void OnReturnTomainMenuPressed()
	{
        GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
        GetTree().Paused = false;
    }
	private void OnExitPressed()
	{
		GetTree().Quit();
    }

    private void resume()
	{
		GetTree().Paused = false;
        this.Visible = false;
    }
}
