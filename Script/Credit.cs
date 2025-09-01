using Godot;
using System;

public partial class Credit : ColorRect
{

	[Export]
	float creditsSpeed = 5f;
    private string filename = "res://Assets/Text/CreditsMainText.txt";
    bool isIgen = false;

    RichTextLabel creditList;
    Sprite2D forgIgames;
    string tempTestText;

    public override void _Ready()
	{
        NodesInit();        
        tempTestText = LoadFromFile();
        creditList.Text = tempTestText;
        creditList.Text += tempTestText;
        creditList.Text += tempTestText;
        creditList.Text += tempTestText;
        creditList.Text += tempTestText;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (isIgen)
        {
            creditList.GlobalPosition = new Vector2 (creditList.GlobalPosition.X, creditList.GlobalPosition.Y - creditsSpeed);  
        }
        if (Input.IsActionJustPressed("pause"))
        {
            GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
        }

    }

    private void OnImageTimerTimeout()
    {

        isIgen = true;
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
