using Godot;
using System;

public partial class Credit : Label
{

	[Export]
	float creditsSpeed = 0.7f;	
	public override void _Ready()
	{
		//this.GlobalPosition = new Vector2();
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
	  
}
