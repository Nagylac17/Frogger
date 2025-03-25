using Godot;
using System;

public partial class Options : Node2D
{
	const int MasterBusNumber = 0;
	const int SFXBusNumber = 1;
	const int MusicBusNumber = 2;


	HSlider SFXSlider;
	AudioStreamPlayer2D TestSound;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TestSound = GetNode<AudioStreamPlayer2D>("TestSound");

		SFXSlider = GetNode<HSlider>("Panel/SoundContainer/SoundSlider");
		SFXSlider.Value = AudioServer.GetBusVolumeDb(SFXBusNumber)+100;

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	private void OnBackPressed()
	{
			GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
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



}

