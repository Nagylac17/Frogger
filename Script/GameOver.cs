using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class GameOver : Panel
{

    Label score;
    Label HighScore;
    private ConfigFile SC = new ConfigFile();
    private string SCName = "Score.cfg";

    public override void _Ready()
    {

        setPosition();
        SC.Load("user://" + SCName);

        score = GetNode<Label>("MainContainer/ScoreLabels/ScoreAmount");
        score.Text = GameInterface.GetScore();

        HighScore = GetNode<Label>("MainContainer/ScoreLabels/HighScoreAmount");
        HighScore.Text = SetHighScore();


        this.Visible = true;
        SaveScoreToFile();
        GetTree().Paused = true;
    }

    private void SaveScoreToFile()
    {
        SC.Save("user://" + SCName);
    }

    private string SetHighScore()
    {
        if (Convert.ToInt32(score.Text) > Convert.ToInt32(SC.GetValue("HighScore", "Score").ToString()))
        {
            HighScore.Text = score.Text;
            SC.SetValue("HighScore", "Score", HighScore.Text);
            return HighScore.Text;
        }
        else
        {
            return SC.GetValue("HighScore", "Score").ToString();
        }
    }

    public void NoLifeLeft()
    {
        GetTree().Paused = true;
    }
    void OnReturnToMenuPressed()
    {
        GetTree().Paused = false;
        score.Text = "0";
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
