using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public partial class GameInterface : Node2D
{

	static private Label score;
	static private int scoreAmount = 0;
	private static Sprite2D [] FrogLives = new Sprite2D[3];
	private ProgressBar progressBar;
	private Timer remainingTime;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		FrogLives[0] = GetChild<Sprite2D>(4);
		FrogLives[1] = GetChild<Sprite2D>(5);
		FrogLives[2] = GetChild<Sprite2D>(6);
        score = GetChild<Label>(3);
        progressBar = GetChild<ProgressBar>(7);
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
		GD.Print(FrogLives.Count(x => x.Visible == true));
		return FrogLives.Count(x => x.Visible == true);
	}
}
