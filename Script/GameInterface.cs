using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public partial class GameInterface : Node2D
{

	static private Label score;
	static private Label level;
	static private int scoreAmount = 0;
	static private int levelAmount = 1;
	private static Sprite2D [] FrogLives = new Sprite2D[3];
	private ProgressBar progressBar;
	private Timer remainingTime;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        score = GetChild<Label>(3);
        level = GetChild<Label>(5);
		FrogLives[0] = GetChild<Sprite2D>(6);
		FrogLives[1] = GetChild<Sprite2D>(7);
		FrogLives[2] = GetChild<Sprite2D>(8);
        progressBar = GetChild<ProgressBar>(9);
        remainingTime = progressBar.GetChild<Timer>(0);

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	static public void AddScore(int amount)
	{
        scoreAmount += amount;
		score.Text = scoreAmount.ToString();
    }
	static public void Subtract(int amount)
	{
		if (scoreAmount > 0)
		{ 
			scoreAmount -= amount;
			score.Text = scoreAmount.ToString();
		}
    }

	static public void AddLevel(int amount)
	{
        levelAmount += amount;
        level.Text = levelAmount.ToString();
    }

	static public void RemoveLifeSprite()
	{
		FrogLives[LifeRemoveHelper()].Visible = false;		
	}
	static public void AddLifeSprite()
	{
		FrogLives[0].Visible = true;
	}

	private static int LifeRemoveHelper()
	{
		for (int i = 2; i > -1; i--)
		{
			if (FrogLives[i].Visible)
			{
				return i;
			}
		}
		return 2;
	}

	public void OnTimerTimeout()
	{
		progressBar.Value = (progressBar.Value - 1); 
    }

	public static int GetRemaningLife()
	{
		return FrogLives.Count(x => x.Visible == true);
	}
}
