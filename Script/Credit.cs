using Godot;
using System;

public partial class Credit : ColorRect
{

	[Export]
	float creditsSpeed = 1.0f;
    private string filename = "res://Assets/Text/CreditsText.txt";

    RichTextLabel creditList;
    Sprite2D forgIgames;

    public override void _Ready()
	{
        NodesInit();
        creditList.Text = LoadFromFile();        
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

    private void OnImageTimerTimeout()
    {
        GD.Print("haha");
        FadeOutTest();
    }

    private void FadeOutTest()
    {
        for (float i = 0; i < 1; i =  i + 0.1f)
        {
            forgIgames.SelfModulate = new Color(1, 1, 1, i);
        }
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
        forgIgames = GetNode<Sprite2D>("FrogImage");
    }

}
