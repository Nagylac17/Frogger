using Godot;
using System;

public partial class Spawner : Marker2D
{

	Log log = new Log();
    PackedScene scene = GD.Load<PackedScene>("res://Scenes/Log.tscn");
	TileMap tileMap; 
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		SpriteSetup();
		AddLog();
    }
	private void SpriteSetup()
	{
       tileMap = (TileMap)GetNode("/root/Node2D/TileMap");
    }

	void AddLog()
	{
		log = (Log)scene.Instantiate();
		log.GlobalPosition = this.GlobalPosition;
		tileMap.AddChild(log);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	//public override void _Process(double delta)
	//{
	//}
}
