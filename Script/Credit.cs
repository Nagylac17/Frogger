using Godot;
using Godot.Collections;
using System;
using System.IO;

public partial class Credit : ColorRect
{

	[Export]
	float creditsSpeed = 0.8f;

    private string mainTextFileName = "res://Assets/Text/CreditsMainText.txt";
    private string specialFileName = "res://Assets/Text/CreditsSpecialText.txt";
    string mainText;
    string specialText;
    bool isCreditsMoving = true;

    RichTextLabel creditList;
    AudioStreamPlayer2D creditMusicStreamer;
    Timer endTimer;

    public override void _Ready()
	{
        Input.MouseMode = Input.MouseModeEnum.Hidden;
        NodesInit();        
        mainText = LoadFromFile(mainTextFileName);
        specialText = LoadFromFile(specialFileName);
        creditList.Text = mainText + specialText;

        for (int i = 0; i < 5; i++)
        {
            creditList.Text += "[center]Again[/center]" + specialText;
        }
        creditList.Text += "[center]\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n[color=Yellow][b]Credits Music[/b][/color]\n\nGTA Radio - FROG[/center]";

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if(isCreditsMoving)
        { 
            creditList.GlobalPosition = new Vector2 (creditList.GlobalPosition.X, creditList.GlobalPosition.Y - creditsSpeed);  
        }

        if (Input.IsActionJustPressed("pause"))
        {
            Input.MouseMode = Input.MouseModeEnum.Visible;
            GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
        }

    }
    private void OnFrogMusicFinished()
    {
        isCreditsMoving = false;
        endTimer.Start();
    }

    private void OnTextTimerTimeout()
    {

        creditsSpeed = 5f;

    }

    private void OnEndTimerTimeout()
    {
        Input.MouseMode = Input.MouseModeEnum.Visible;
        GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
    }
    private void OnMusicTimerTimeout()
    {
        creditMusicStreamer.Play();
    }


    public string LoadFromFile(string _fileName)
    {

        using var file = Godot.FileAccess.Open(_fileName, Godot.FileAccess.ModeFlags.Read);
        string content = file.GetAsText();
        return content;
    }

    private void NodesInit()
    {
        endTimer = GetNode<Timer>("EndTimer");
        creditList = GetNode<RichTextLabel>("CreditContainer/Credit");
        creditMusicStreamer = GetNode<AudioStreamPlayer2D>("FrogMusic");
    }

}
