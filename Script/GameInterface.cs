using Godot;
using System;
using System.Collections.Generic;

public partial class GameInterface : Node2D
{

	static private Label score;
	static private int scoreAmount = 0;
	private static Sprite2D [] FrogLives = new Sprite2D[3];


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		FrogLives[0] = GetChild<Sprite2D>(4);
		FrogLives[1] = GetChild<Sprite2D>(5);
		FrogLives[2] = GetChild<Sprite2D>(6);
        score = GetChild<Label>(3);
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
}
