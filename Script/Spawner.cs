using Godot;
using Godot.Collections;
using System;

public partial class Spawner : Marker2D
{

	Log log = new Log();
	Car car = new Car();

	[ExportGroup("Spwaned object")]
	[Export]
	public Godot.Collections.Dictionary<string, PackedScene> Assets { get; set; }
	private string[] stringarray = new string[7] { "Log", "LargeLog", "BlueCar", "RedCar", "WhiteCar", "Turtle", "Aligator" };

	[Export]
	string SpawnLocation;

	Random rnd = new Random();
	TileMap tileMap;
	Timer spawnTimer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TimerSetup();
        TileMapSetup();
	}

	private void TileMapSetup()
	{
		tileMap = (TileMap)GetNode("/root/Node2D/TileMap");
	}

	private void TimerSetup()
	{
        spawnTimer = GetChild<Timer>(0);
        spawnTimer.WaitTime = rnd.Next(1, 6);
        spawnTimer.Start();
    }

	private void OnTimeout()
	{
		switch (ChooseAsset())
		{
			case "Log" :
				{
					AddLog();
					break;
				}
			case "BlueCar" :
				{
                    AddBlueCar();
					break;
				}
		}
	}

	private string ChooseAsset()
	{
		string asset = "";

		switch (SpawnLocation)
		{
			case "Water":
				{
					asset = "Log"; //Ide meg kell variaciot adni
					break;
				}
			case "Road":
				{
					asset = "BlueCar"; // Itt kell megadni melyik tombbol szedje az adatokat
					break;
				}
	
		}


		return asset;
	}

    void AddLog()
	{
		//log = (Log)Assets[0].Instantiate();
		log = (Log)Assets["Log"].Instantiate();
		log.GlobalPosition = this.GlobalPosition;
        log.Direction = this.GlobalPosition.X < 100 ? -1 : 1;
		tileMap.AddChild(log);
	}
    void AddBlueCar()
	{
		//log = (Log)Assets[0].Instantiate();
		car = (Car)Assets["BlueCar"].Instantiate();
		car.GlobalPosition = this.GlobalPosition;
		car.Direction = this.GlobalPosition.X < 100 ? -1 : 1;
		tileMap.AddChild(car);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	//public override void _Process(double delta)
	//{
	//}
}
