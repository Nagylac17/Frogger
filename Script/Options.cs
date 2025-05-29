using Godot;
using Godot.Collections;
using System;

public partial class Options : Node2D
{
	const int MasterBusNumber = 0;
	const int SFXBusNumber = 1;
	const int MusicBusNumber = 2;
    Dictionary<string, Vector2I> Resolutions = new Dictionary<string, Vector2I>();


	OptionButton ResolutionOptionButton;
	HSlider SFXSlider;
	AudioStreamPlayer2D TestSound;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		SetResolutionDictionary();

        TestSound = GetNode<AudioStreamPlayer2D>("TestSound");
		
		SFXSlider = GetNode<HSlider>("Panel/SoundContainer/SoundSlider");
		ResolutionOptionButton = GetNode<OptionButton>("Panel/VideoContainer/ResolutionButton");
		SFXSlider.Value = AudioServer.GetBusVolumeDb(SFXBusNumber)+100;

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	private void SetResolutionDictionary()
	{
		Resolutions.Add("1280x720", new Vector2I(1280,720));
		Resolutions.Add("1600x900", new Vector2I(1600,900));
		Resolutions.Add("1920x1080", new Vector2I(1920,1080));
		Resolutions.Add("2560x1440", new Vector2I(2560,1440));
	}

	private void OnBackPressed()
	{
			GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
    }

	private void OnAplyPressed()
	{
		
	}

    public void OnSFXValueChange(float _value)
	{
		if (_value == 0)
		{
			AudioServer.SetBusMute(SFXBusNumber,true);
			return;
		}

		if (AudioServer.IsBusMute(SFXBusNumber) == true)
		{
			AudioServer.SetBusMute(SFXBusNumber,false);
		}

		float decibal = ConvertPercentageToDecibal(_value);
		AudioServer.SetBusVolumeDb(SFXBusNumber, decibal);
		TestSound.Play();

	}

	private float ConvertPercentageToDecibal(float percentage) //Godot linear to decibal method missing int this version so this is a workaround
	{
		float scale = 20f;
		float divisor = 50f;

		return scale * (float)Math.Log10(scale / divisor);
	}

	public void OnResolutionButtonItemSelected(int i)
	{
		//GD.Print(ResolutionOptionButton.Text);
		DisplayServer.WindowSetSize(Resolutions[ResolutionOptionButton.Text]);
		DisplayServer.WindowSetPosition(new Vector2I(0,0));
	}



}

