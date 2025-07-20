using Godot;
using System;
using System.IO;
using System.Xml.Linq;

public partial class GameStart : Node2D
{
	LoadGame loadGame = new LoadGame();
    private ConfigFile SC = new ConfigFile();
    private string SCName = "Score.cfg";
  
    Label VersionLabel;
	Label HighScore;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
			
			VersionLabel =  GetNode<Label>("VersionNumber");
			HighScore =  GetNode<Label>("HighScore");
			VersionLabel.Text = "Alpha V6";
            FileCreator.CreateCFGFile();
            FileCreator.CreateHighScoreFile();

        loadGame.LoadSave();
        GD.Print(OS.GetUserDataDir() + "/" + "settings.cfg");
        SC.Load("user://" + SCName);
        HighScore.Text = "High Score:" + SC.GetValue("HighScore", "Score").ToString();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
