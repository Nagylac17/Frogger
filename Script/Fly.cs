using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;

public partial class Fly : Node2D
{

	Timer despawnTimer;
	Timer spawnTimer;
	Vector2 originalPos;
	int Direction;
    int scoreValue = 100 * 1; // szinthez kell kotni a pontnyereseget

	Node originalParent;
	Node2D pickedLog;

    [Export]
    PackedScene showScorePackedScene;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		originalPos = GlobalPosition;
		originalParent = GetParent();
		despawnTimer = GetNode<Timer>("DeSpawnTimer");
		spawnTimer = GetNode<Timer>("SpawnTimer");
        //SFXSlider = GetNode<HSlider>("Panel/SoundContainer/SoundSlider");

    }

	private void OnSpawnTimerTimeout()
	{
		GetSpwanableLogs();
		SpawnOnLog();

    }

	private void OnDeSpawnTimerTimeout()
	{
		SetFlyOriginalState();
    }

    private void OnAreaEntered(Area2D _area)
    {

        ScorePointLabelSpwan();
        SetFlyOriginalState();
        GameInterface.AddScore(100);

    }

    private void ScorePointLabelSpwan()
    {
        EarnedScore showScore = (EarnedScore)showScorePackedScene.Instantiate();
        showScore.score = Convert.ToString(scoreValue);
        showScore.GlobalPosition = pickedLog.GlobalPosition;
        originalParent.AddChild(showScore);
    }

    private void SpawnOnLog()
    {
      
            if (pickedLog is not null)
            {
                GlobalPosition = pickedLog.GlobalPosition;
         
                Reparent(pickedLog);
                despawnTimer.Start();
                spawnTimer.Stop();
            }  
     
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



	private void SetFlyOriginalState()
	{
        CallDeferred("reparent",originalParent);
        GlobalPosition = originalPos;
        spawnTimer.Start();
		despawnTimer.Stop();
    }
}
