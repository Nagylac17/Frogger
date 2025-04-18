using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Spawner : Marker2D
{

	Log log = new Log();
	Car car = new Car();

	[ExportGroup("Spawned object")]
	[Export]
	public Godot.Collections.Dictionary<string, PackedScene> Assets { get; set; }
	private string[] RoadSpawnAssets = new string[3] {"BlueCar", "RedCar", "WhiteCar"};
	private string[] WaterSpawnAssets = new string[4] { "Log", "LargeLog","Turtle", "Aligator" };

	[Export]
	string SpawnLocation;

	Random rnd = new Random();
	TileMap tileMap;
	Timer spawnTimer;
	int[] spawnCounter = new int[500];
	List<int> UsedSpawnNameNumber = new List<int>(); // Csak LOG objekteket tartalmaz majd ezt kell megcsinalni, hogy minden mast is kezeljen ha kell
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SpawnCounterFill();
        TimerSetup();
        TileMapSetup();
		
	}

	private void SpawnCounterFill()
	{
        for (int i = 1; i < 501; i++)
        {
            spawnCounter[i - 1] = i;

        }
    }

	private void TileMapSetup()
	{
		tileMap = (TileMap)GetNode("/root/Node2D/TileMap");
	}

	private void TimerSetup()
	{
        spawnTimer = GetChild<Timer>(0);
		SpawnTime();
        spawnTimer.Start();
    }

	private void SpawnTime()
	{
        spawnTimer.WaitTime = rnd.Next(1, 6); // spawn ido valtoztatas
    }

	private void OnTimeout()
	{



        switch (ChooseAsset())
		{
			case "Log" :
				{
					SpawnTime();
                    AddLog();
					break;
				}
			case "BlueCar" :
				{
					SpawnTime();
                    AddBlueCar();
					break;
				}
			case "RedCar" :
				{
					SpawnTime();
                    AddRedCar();
					break;
				}
			case "WhiteCar" :
				{
					SpawnTime();
                    AddWhiteCar();
					break;
				}
		}
	}

	private string ChooseName()
	{
		int pickedNumber;
		do
		{
			pickedNumber = rnd.Next(1, 500);
		}
		while (UsedSpawnNameNumber.Contains(pickedNumber));

		UsedSpawnNameNumber.Add(pickedNumber);

        string name = spawnCounter[pickedNumber].ToString();

        return name;
	}

	private void ClearUsedSpawnNameNumberList()
	{
		UsedSpawnNameNumber = UsedSpawnNameNumber.Except(DeSpawner.DeletedAssetNumbers).ToList();
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
					asset = RoadSpawnAssets[rnd.Next(0,3)]; // Itt kell megadni melyik tombbol szedje az adatokat
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
		log.Name = "Log" + ChooseName();
		tileMap.AddChild(log);
		//GD.Print(log.Name);
	}
    void AddBlueCar()
	{
		//log = (Log)Assets[0].Instantiate();
		car = (Car)Assets["BlueCar"].Instantiate();
		car.GlobalPosition = this.GlobalPosition;
		car.velocity = 0.3f;
		car.Direction = this.GlobalPosition.X < 100 ? -1 : 1;
		tileMap.AddChild(car);
	}
    void AddRedCar()
	{
		//log = (Log)Assets[0].Instantiate();
		car = (Car)Assets["RedCar"].Instantiate();
		car.GlobalPosition = this.GlobalPosition;
		car.velocity = 0.5f;
		car.Direction = this.GlobalPosition.X < 100 ? -1 : 1;
		tileMap.AddChild(car);
	}
    void AddWhiteCar()
	{
		//log = (Log)Assets[0].Instantiate();
		car = (Car)Assets["WhiteCar"].Instantiate();
		car.GlobalPosition = this.GlobalPosition;
		car.velocity = 0.1f;
		car.Direction = this.GlobalPosition.X < 100 ? -1 : 1;
		tileMap.AddChild(car);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	//public override void _Process(double delta)
	//{
	//}
}
