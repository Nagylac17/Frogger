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
    string paidText = "Pay 5 euros to have your name appear!";
    int tempSzamlalo = 0;

    RichTextLabel creditList;
    AudioStreamPlayer2D creditMusicStreamer;

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
        //creditList.Text += "GTA Radio - FROG";

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
    private void OnFrogMusicFinished()
    {
        Input.MouseMode = Input.MouseModeEnum.Visible;
        GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
    }

    private void OnTextTimerTimeout()
    {

        creditsSpeed = 5f;

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
        creditList = GetNode<RichTextLabel>("CreditContainer/Credit");
        creditMusicStreamer = GetNode<AudioStreamPlayer2D>("FrogMusic");
    }

}
