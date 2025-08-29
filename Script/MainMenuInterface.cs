using Godot;
using System;
using System.IO;

public partial class MainMenuInterface : Node2D
{

    Label HighScore;
    private ConfigFile SC = new ConfigFile();
    private string SCName = "Score.cfg";
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        SC.Load("user://" + SCName);
        HighScore =  GetNode<Label>("Score");
        HighScore.Text =  SC.GetValue("HighScore", "Score").ToString();

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
        GetTree().ChangeSceneToFile("res://Scenes/Credits.tscn");
    }
    private void OnExitPressed()
    {
        GetTree().Quit();
    }
}
