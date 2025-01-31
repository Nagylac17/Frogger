using Godot;
using System;

public static partial class GameOver
{


    public static void NoLifeLeft(Node node)
    {
        node.GetTree().Paused = true;
    }
        

}
