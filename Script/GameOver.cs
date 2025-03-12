using Godot;
using System;
using System.Runtime.CompilerServices;

public  partial class GameOver : Panel
{


    //Kell ide egy label is ami tartalmazza az elert pontot
    //Signal kell ide legkozelebb meg kell tanulni hogy kell
    Label score;

    public override void _Ready()
    {
        //NoLifeLeft();
        setPosition();
        score = (Label)(GetChild(1).GetChild(1).GetChild(1));
        score.Text = GameInterface.GetScore();
        this.Visible = true;
        GetTree().Paused = true;
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

    private void setPosition()
    {
        float x = (GetViewport().GetVisibleRect().Size.X - this.Size.X) / 2;
        float y = (GetViewport().GetVisibleRect().Size.Y - this.Size.Y) / 2;
        this.GlobalPosition = new Vector2(x,y);
    }

}
