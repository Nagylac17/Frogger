using Godot;
using System;
using System.IO;
using System.Xml.Linq;

public partial class GameStart : Node2D
{
	LoadGame loadGame = new LoadGame();
	Label VersionLabel; 
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
			VersionLabel =  GetNode<Label>("VersionNumber");
			VersionLabel.Text = "Alpha";
            FileCreator.CreateCFGFile();
            FileCreator.CreateHighScoreFile();

        loadGame.LoadSave();
        GD.Print(OS.GetUserDataDir() + "/" + "settings.cfg");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
