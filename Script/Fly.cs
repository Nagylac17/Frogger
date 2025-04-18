using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;

public partial class Fly : Node2D
{

	Timer spawnTimer;
	Vector2 originalPos;
	int Direction;

	Node originalParent;
	Node2D pickedLog;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		originalPos = GlobalPosition;
		originalParent = GetParent();


    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		

    }

	private void SpawnOnLog()
	{
		if (pickedLog != null)
		{ 
			GlobalPosition = pickedLog.GlobalPosition;
			Reparent(pickedLog);
		}
	}
	private void OnTimerTimeout()
	{
		GetSpwanableLogs();
		SpawnOnLog();
;
	}

	private void GetSpwanableLogs()
	{
        Random rnd = new Random();
        var tempMap = originalParent.GetNode<TileMap>("TileMap").GetChildren();

        List<Node2D> tempChildList = new List<Node2D>();

        foreach (Node2D child in tempMap)
        {
            if (child.Name.ToString().Contains("Log") && child.GlobalPosition.X < 457 && child.GlobalPosition.X > 185)
            {
                tempChildList.Add(child);
            }
        }
        if (tempChildList.Count != 0)
        {
            pickedLog = tempChildList[rnd.Next(0, tempChildList.Count)];

        }
    }

	private void OnAreaEntered(Area2D _area)
	{
		GlobalPosition = originalPos; 
	}
}
