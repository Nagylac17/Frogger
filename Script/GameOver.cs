using Godot;
using System;
using System.Runtime.CompilerServices;

public static partial class GameOver
{
   static PopupPanel EndingScreen;



    public static void NoLifeLeft(Node node)
    {
        node.GetTree().Paused = true;
    }

    public static void ShowGameOver()
    {
        EndingScreen.GetInstanceId();
    }
        

}
