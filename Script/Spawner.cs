using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Spawner : Marker2D
{

	Log log = new Log();
	Car car = new Car();
	Turtle turtle = new Turtle();
	Aligator aligator = new Aligator();
	RayCast2D rightRayCast = new RayCast2D();
	RayCast2D leftRayCast = new RayCast2D();


	bool IsThisSpawnerActive = true;

	[ExportGroup("Spawned object")]
	[Export]
	public Godot.Collections.Dictionary<string, PackedScene> Assets { get; set; }
	private string[] RoadSpawnAssets = new string[3] { "BlueCar", "RedCar", "WhiteCar" };
	private string[] WaterSpawnAssets = new string[5] { "Log", "LargeLog", "Turtle", "Aligator","Trunk" };

	[Export]
	string SpawnLocation;

	Random rnd = new Random();
	TileMap tileMap;
	Timer spawnTimer;
	int[] spawnCounter = new int[500];
	List<int> UsedSpawnNameNumber = new List<int>(); 
													 // Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SpawnCounterFill();
		TimerSetup();
		TileMapSetup();
		RayCastsSetup();

    }

    public override void _Process(double delta)
    {

	}

    private void RayCastsSetup()
	{
        rightRayCast = GetNode<RayCast2D>("RightRayCast2D");
        leftRayCast = GetNode<RayCast2D>("LeftRayCast2D");
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
		spawnTimer = GetNode<Timer>("Timer");
		SpawnTime();
		spawnTimer.Start();
	}

	private void SpawnTime()
	{
		spawnTimer.WaitTime = rnd.Next(1, GameInterface.GetCurretLevel() < 2? 5:4); 
	}

	private void OnTimeout()
	{

		if (!rightRayCast.IsColliding() && !leftRayCast.IsColliding())
		{
			switch (ChooseAsset())
			{
				case "Log":
					{
						AddLog();
						break;
					}
				case "Trunk":
					{
						AddTrunk();
						break;
					}
				case "Turtle":
					{
						AddTurtle(rnd.Next(1, 6));
						break;
					}
				case "Aligator":
					{
						AddAligator();
						break;
					}
				case "BlueCar":
					{
						AddBlueCar();
						break;
					}
				case "RedCar":
					{
						AddRedCar();
						break;
					}
				case "WhiteCar":
					{
						AddWhiteCar();
						break;
					}
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
					switch (rnd.Next(1, GameInterface.GetCurretLevel() > 1 ? 4 : 5))
					{
						case 1:
                            asset = "Turtle";
                            break;
						case 2:
                            asset = "Log";
							break;
						case 3:
							asset = "Aligator";
							break;
						case 4:
							asset = "Trunk";
							break;
						default:
							asset = "Log";
							break;
                    }
						
					break;
				}
			case "Road":
				{
					asset = RoadSpawnAssets[rnd.Next(0, 3)];
					break;
				}

		}


		return asset;
	}
    #region AddSpanwableObject

    #region Water
    void AddLog()
	{
		log = (Log)Assets["Log"].Instantiate();
		log.GlobalPosition = GlobalPosition;
        log.direction = GlobalPosition.X < 100 ? -1 : 1;
		log.Name = "Log" + ChooseName();
		tileMap.AddChild(log);
	}
    void AddTrunk()
	{
		log = (Log)Assets["Trunk"].Instantiate();
		log.GlobalPosition = GlobalPosition;
        log.direction = GlobalPosition.X < 100 ? -1 : 1;
		log.Name = "Log" + ChooseName();
		tileMap.AddChild(log);
	}
	void AddTurtle(int amount)
	{
        var randomDiving = rnd.Next(1, 3) == 1 ? false : true;
        for (int i = 0; i < amount; i++)
		{
			turtle = (Turtle)Assets["Turtle"].Instantiate();
			var texturesize = turtle.Texture.GetWidth()/turtle.Hframes;
			turtle.GlobalPosition = GlobalPosition + new Vector2(i * texturesize,0);
			turtle.direction = GlobalPosition.X < 100 ? -1 : 1;
			turtle.IsdivingTurtle = randomDiving;
            tileMap.AddChild(turtle);
		}
    }

	void AddAligator()
	{
        aligator = (Aligator)Assets["Aligator"].Instantiate();
        aligator.GlobalPosition = GlobalPosition;
        aligator.direction = GlobalPosition.X < 100 ? -1 : 1;
        aligator.Name = "Aligator" + ChooseName();
        tileMap.AddChild(aligator);
    }

    #endregion
    #region Road
    void AddBlueCar()
	{
		car = (Car)Assets["BlueCar"].Instantiate();
		car.GlobalPosition = this.GlobalPosition;
		car.velocity = 0.3f;
		car.direction = this.GlobalPosition.X < 100 ? -1 : 1;
		tileMap.AddChild(car);
	}
    void AddRedCar()
	{
		car = (Car)Assets["RedCar"].Instantiate();
		car.GlobalPosition = this.GlobalPosition;
		car.velocity = 0.5f;
		car.direction = this.GlobalPosition.X < 100 ? -1 : 1;
		tileMap.AddChild(car);
	}
    void AddWhiteCar()
	{
		car = (Car)Assets["WhiteCar"].Instantiate();
		car.GlobalPosition = this.GlobalPosition;
		car.velocity = 0.1f;
		car.direction = this.GlobalPosition.X < 100 ? -1 : 1;
		tileMap.AddChild(car);
	}
    #endregion

    #endregion

}
