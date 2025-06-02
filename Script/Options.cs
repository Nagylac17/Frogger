using Godot;
using Godot.Bridge;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Options : Node2D
{
	const int MasterBusNumber = 0;
	const int SFXBusNumber = 1;
	const int MusicBusNumber = 2;
    List<OBJResolutions> Resolutions = new List<OBJResolutions> 
	{
        new OBJResolutions { Sres = "1280x720", VIres = new Vector2I(1280, 720)},
        new OBJResolutions { Sres = "1600x900", VIres = new Vector2I(1600, 900)},
        new OBJResolutions { Sres = "1920x1080", VIres = new Vector2I(1920, 1080)},
        new OBJResolutions { Sres = "2560x1440", VIres = new Vector2I(2560, 1440)}
    };
	SaveGame saveGame = new SaveGame();


	OptionButton ResolutionOptionButton;
	HSlider SFXSlider;
	HSlider MasterSlider;
	HSlider MusicSlider;
	AudioStreamPlayer2D TestSound;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

        MasterSlider = GetNode<HSlider>("Panel/SoundContainer/MasterSlider");
		SFXSlider = GetNode<HSlider>("Panel/SoundContainer/SoundSlider");
		MusicSlider = GetNode<HSlider>("Panel/SoundContainer/MusicSlider");
        ResolutionOptionButton = GetNode<OptionButton>("Panel/VideoContainer/ResolutionButton");

        TestSound = GetNode<AudioStreamPlayer2D>("TestSound");

		ResolutionOptionButton.Select(Resolutions.IndexOf(Resolutions.Where(re => re.VIres == DisplayServer.WindowGetSize()).FirstOrDefault()));
		MasterSlider.Value = ConvertDecibelToPercentage(AudioServer.GetBusVolumeDb(MasterBusNumber));
		SFXSlider.Value = ConvertDecibelToPercentage(AudioServer.GetBusVolumeDb(SFXBusNumber));
		MusicSlider.Value = ConvertDecibelToPercentage(AudioServer.GetBusVolumeDb(MusicBusNumber));

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

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
        //GD.Print("Master"+MasterSlider.Value);
        //
        //GD.Print("Music"+MusicSlider.Value);
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
        GD.Print("SFX" + SFXSlider.Value);
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

    private float ConvertPercentageToDecibel(float _percentage) //Godot linear to decibal method missing int this version so this is a workaround
	{

		float scale = 5;
		float decibel = scale * (float)Math.Log10(_percentage);

		return decibel;
    }

	private float ConvertDecibelToPercentage(float _decibel)
	{
		if (_decibel == 0)
		{
			return 0;
		}
        float scale = 5;
		float percentage = (float)Math.Pow(10, _decibel / scale);

        return percentage;

    }

	public void OnResolutionButtonItemSelected(int i)
	{
		//GD.Print(ResolutionOptionButton.Text);
		DisplayServer.WindowSetSize(Resolutions.Where(rs=>rs.Sres == ResolutionOptionButton.Text).FirstOrDefault().VIres);
		DisplayServer.WindowSetPosition(new Vector2I(0,0));
	}

	class OBJResolutions
	{
		public string Sres;
		public Vector2I VIres;
	}


}

