using Godot;
using Godot.Collections;
using System;

public partial class Spawner : Marker2D
{

	Log log = new Log();

	[ExportGroup("Spwaned object")]
	//[Export]
	//public Godot.Collections.Array<PackedScene> Assets { get; set; }
	[Export]
	public Godot.Collections.Dictionary<string, PackedScene> Assets { get; set; }
	private string[] stringarray = new string[7] { "Log", "LargeLog", "BlueCar", "RedCar", "WhiteCar", "Turtle", "Aligator" };


	TileMap tileMap;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TileMapSetup();
	}
	private void TileMapSetup()
	{
		tileMap = (TileMap)GetNode("/root/Node2D/TileMap");
	}

	private void OnTimeout()
	{
		string kivalasztottAsset = "Log";

		switch (kivalasztottAsset)
		{
			case "Log" :
				{
					AddLog();
					break;
				}
		}
	}

    void AddLog()
	{
		//log = (Log)Assets[0].Instantiate();
		log = (Log)Assets["Log"].Instantiate();
		log.GlobalPosition = this.GlobalPosition;		
		tileMap.AddChild(log);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	//public override void _Process(double delta)
	//{
	//}
}
