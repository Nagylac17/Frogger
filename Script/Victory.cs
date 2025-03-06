using Godot;
using System;
using System.Collections.Generic;

public partial class Victory : Node
{
	static int completedRound = 0;
	static List<Leaf> Leaves = new List<Leaf>();

	public static void AddWFrog(Leaf leaf)
	{
		Leaves.Add(leaf);
		if (Leaves.Count == 6)
		{
			MapWin();
		}
	}

	private static void MapWin()
	{
		completedRound++;

		foreach (var Leaf in Leaves)
		{
			Leaf.HideWFrog();
		}
        GameInterface.AddLevel(1);
        Leaves.Clear();
    }

	public static float VelocityIncrease()
	{
		float velocityInc = completedRound / 2;
		return velocityInc;
	}



}
