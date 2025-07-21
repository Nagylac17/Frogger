using Godot;
using System;
using System.Collections.Generic;

public partial class Victory : Node
{
	static int completedRound = 0;
    private static Node2D MainNode;
    private static PackedScene VictoryScene;
	private static Panel VictoryScreenNode;

    static List<Leaf> Leaves = new List<Leaf>();


	public static void AddWFrog(Leaf leaf)
	{
		Leaves.Add(leaf);
		GameInterface.ResetTimer();

        if (Leaves.Count == 6)
		{
			MapWin();
		}
	}

	private static void MapWin()
	{

        if (completedRound == 3)
        {
            GameWin();
        }
        completedRound++;

		foreach (var Leaf in Leaves)
		{
			Leaf.HideWFrog();
		}
        GameInterface.AddLevel(1);
        Leaves.Clear();
    }

	private static void GameWin()
	{
        MainNode = (Node2D)Leaves[0].GetParent();
        VictoryScene = GD.Load<PackedScene>("res://Scenes/VictoryScreen.tscn");
		GameInterface.AddScore(1000);
		VictoryScreenNode = (Panel)VictoryScene.Instantiate();
        MainNode.AddChild(VictoryScreenNode);
    }

	public static float VelocityIncrease()
	{
		float velocityInc = (completedRound + 0.02f) / 10;
		return velocityInc;
	}



}
