using Godot;
using System;

public partial class Credit : RichTextLabel
{

	[Export]
	float creditsSpeed = 0.6f;
    private string filename = "res://Assets/Text/CreditsText.txt";
    public override void _Ready()
	{
        Text = LoadFromFile();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.GlobalPosition = new Vector2 (this.GlobalPosition.X, this.GlobalPosition.Y - creditsSpeed);
        if (Input.IsActionJustPressed("pause"))
        {
            GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");

        }

    }

    public string LoadFromFile()
    {

        using var file = Godot.FileAccess.Open(filename, Godot.FileAccess.ModeFlags.Read);
        string content = file.GetAsText();
        return content;
    }

}
