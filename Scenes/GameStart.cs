using Godot;
using System;
using System.Xml.Linq;

public partial class GameStart : Node2D
{
	LoadGame loadGame = new LoadGame();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		loadGame.LoadSave();
        GD.Print(OS.GetUserDataDir() + "/" + "settings.cfg");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
