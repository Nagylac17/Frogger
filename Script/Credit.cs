using Godot;
using Godot.Collections;
using System;
using System.IO;

public partial class Credit : ColorRect
{

	[Export]
	float creditsSpeed = 0.8f;

    private string filename = "res://Assets/Text/CreditsMainText.txt";
    string tempTestText;

    RichTextLabel creditList;
    AudioStreamPlayer2D creditMusicStreamer;

    public override void _Ready()
	{
        NodesInit();        
        tempTestText = LoadFromFile();
        creditList.Text = tempTestText;
        for (int i = 0; i < 15; i++)
        {
        creditList.Text += tempTestText;

        }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
 
            creditList.GlobalPosition = new Vector2 (creditList.GlobalPosition.X, creditList.GlobalPosition.Y - creditsSpeed);  

        if (Input.IsActionJustPressed("pause"))
        {
            GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
        }

    }

    private void OnTextTimerTimeout()
    {

        creditsSpeed = 5f;

    }
    private void OnMusicTimerTimeout()
    {
        creditMusicStreamer.Play();
    }


    public string LoadFromFile()
    {

        using var file = Godot.FileAccess.Open(filename, Godot.FileAccess.ModeFlags.Read);
        string content = file.GetAsText();
        return content;
    }

    private void NodesInit()
    {
        creditList = GetNode<RichTextLabel>("CreditContainer/Credit");
        creditMusicStreamer = GetNode<AudioStreamPlayer2D>("FrogMusic");
    }

}
