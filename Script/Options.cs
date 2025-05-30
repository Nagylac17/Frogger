using Godot;
using Godot.Collections;
using System;

public partial class Options : Node2D
{
	const int MasterBusNumber = 0;
	const int SFXBusNumber = 1;
	const int MusicBusNumber = 2;
    Dictionary<string, Vector2I> Resolutions = new Dictionary<string, Vector2I>();
	SaveGame saveGame = new SaveGame();


	OptionButton ResolutionOptionButton;
	HSlider SFXSlider;
	HSlider MasterSlider;
	HSlider MusicSlider;
	AudioStreamPlayer2D TestSound;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		SetResolutionDictionary();

        TestSound = GetNode<AudioStreamPlayer2D>("TestSound");
		
		MasterSlider = GetNode<HSlider>("Panel/SoundContainer/MasterSlider");
		SFXSlider = GetNode<HSlider>("Panel/SoundContainer/SoundSlider");
		MusicSlider = GetNode<HSlider>("Panel/SoundContainer/MusicSlider");

        
        ResolutionOptionButton = GetNode<OptionButton>("Panel/VideoContainer/ResolutionButton");
		MasterSlider.Value = ConvertDecibelToPercentage(AudioServer.GetBusVolumeDb(MasterBusNumber));
		SFXSlider.Value = ConvertDecibelToPercentage(AudioServer.GetBusVolumeDb(SFXBusNumber));
		MusicSlider.Value = ConvertDecibelToPercentage(AudioServer.GetBusVolumeDb(MusicBusNumber));

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

		saveGame.SetVideoConfig();
		saveGame.SetAudioConfig();
		saveGame.SaveConfig();

    }

	private void AudioValueChange(int AudioBusNumber, float _value)
	{
        if (_value == 0)
        {
            AudioServer.SetBusMute(AudioBusNumber, true);
            return;
        }

        if (AudioServer.IsBusMute(AudioBusNumber) == true)
        {
            AudioServer.SetBusMute(AudioBusNumber, false);
        }

        float decibal = ConvertPercentageToDecibel(_value);
        AudioServer.SetBusVolumeDb(AudioBusNumber, decibal);
        TestSound.Play();
    }

    public void OnSFXValueChange(float _value)
	{
		AudioValueChange(SFXBusNumber, _value);

	}
	public void OnMasterValueChange(float _value)
	{
        AudioValueChange(MasterBusNumber, _value);
    }
	public void OnMusicValueChange(float _value)
	{
        AudioValueChange(MusicBusNumber, _value);
    }

    private float ConvertPercentageToDecibel(float percentage) //Godot linear to decibal method missing int this version so this is a workaround
	{
		
		float scale = 20f;
		float decibel = scale * (float)Math.Log10(scale / percentage) * -1;
		return decibel;
		//return Mathf.LinearToDb(percentage);
    }

	private float ConvertDecibelToPercentage(float Decibel)
	{
        float scale = 20f;
		float decibel = scale / (float)Math.Pow(10, (Decibel / scale)) * -1;
        //float divisor = 50f;
		 
        return  decibel;

    }

	public void OnResolutionButtonItemSelected(int i)
	{
		//GD.Print(ResolutionOptionButton.Text);
		DisplayServer.WindowSetSize(Resolutions[ResolutionOptionButton.Text]);
		DisplayServer.WindowSetPosition(new Vector2I(0,0));
	}



}

