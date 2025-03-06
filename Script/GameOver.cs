using Godot;
using System;
using System.Runtime.CompilerServices;

public  partial class GameOver : Panel
{


    //Kell ide egy label is ami tartalmazza az elert pontot
    //Signal kell ide legkozelebb meg kell tanulni hogy kell

    public override void _Ready()
    {
        //NoLifeLeft();
        this.Visible = true;
    }

    public void NoLifeLeft()
    {
        GetTree().Paused = true;
    }
    void OnReturnToMenuPressed()
    {
        GetTree().Paused = false;
        GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
    }

    void OnExitGamePressed()
    {
        GetTree().Quit();
    } 

}
