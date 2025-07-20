using Godot;
using Godot.Bridge;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

	List<ObjWindowMode> WindowsModes = new List<ObjWindowMode>
	{
		new ObjWindowMode { sMode = "Fullscreen", DsMode = DisplayServer.WindowMode.Fullscreen },
		new ObjWindowMode { sMode = "Windowed",DsMode = DisplayServer.WindowMode.Windowed },

	};
	SaveGame saveGame = new SaveGame();


	OptionButton ResolutionOptionButton;
	OptionButton WindowModeOptionButton;
	HSlider SFXSlider;
	HSlider MasterSlider;
	HSlider MusicSlider;
	AudioStreamPlayer2D TestSound;
	CheckBox BorderlessCheckBox;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

        MasterSlider = GetNode<HSlider>("Panel/SoundContainer/MasterSlider");
		SFXSlider = GetNode<HSlider>("Panel/SoundContainer/SoundSlider");
		MusicSlider = GetNode<HSlider>("Panel/SoundContainer/MusicSlider");
        ResolutionOptionButton = GetNode<OptionButton>("Panel/VideoContainer/ResolutionButton");
		WindowModeOptionButton = GetNode<OptionButton>("Panel/VideoContainer/WindowmodeButton");

        TestSound = GetNode<AudioStreamPlayer2D>("TestSound");
		setResButton();
        WindowModeOptionButton.Select(WindowsModes.IndexOf(WindowsModes.Where(re => re.DsMode == DisplayServer.WindowGetMode()).FirstOrDefault()));
		MasterSlider.Value = ConvertDecibelToPercentage(AudioServer.GetBusVolumeDb(MasterBusNumber));
		SFXSlider.Value = ConvertDecibelToPercentage(AudioServer.GetBusVolumeDb(SFXBusNumber));
		MusicSlider.Value = ConvertDecibelToPercentage(AudioServer.GetBusVolumeDb(MusicBusNumber));		 

    }

	private void setResButton()
	{
		if (DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Fullscreen)
		{
			ResolutionOptionButton.Disabled = true;
		}
		else
		{ 
			ResolutionOptionButton.Select(Resolutions.IndexOf(Resolutions.Where(re => re.VIres == DisplayServer.WindowGetSize()).FirstOrDefault()));
		}
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	private void OnWindowModeButtonItemSelected(int i)
	{
        if (WindowsModes.Where(rs => rs.sMode == WindowModeOptionButton.Text).FirstOrDefault().DsMode == DisplayServer.WindowMode.Fullscreen)
		{
			SetFullscreanWindowMode();
            GD.Print(DisplayServer.WindowGetSize());

        }
        else
        {
			SetWindowMode();
        }

    }

	private void SetFullscreanWindowMode()
	{
	
        DisplayServer.WindowSetMode(DisplayServer.WindowMode.ExclusiveFullscreen);
		ResolutionOptionButton.Disabled = true;

    }
    private void SetWindowMode()
	{
        ResolutionOptionButton.Disabled = false;
		DisplayServer.WindowSetSize(new Vector2I(1280, 720));
        DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
        DisplayServer.WindowSetPosition(new Vector2I(20, 20));
        ResolutionOptionButton.Select(Resolutions.IndexOf(Resolutions.Where(re => re.VIres == new Vector2I(1280, 720)).FirstOrDefault()));
		
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
        
        if (_value == -70)
        {
            AudioServer.SetBusMute(AudioBusNumber, true);
            AudioServer.SetBusVolumeDb(AudioBusNumber, -70);
            return;
        }

        if (AudioServer.IsBusMute(AudioBusNumber) == true)
        {
            AudioServer.SetBusMute(AudioBusNumber, false);
        }

		float decibal = _value;// ConvertPercentageToDecibel(_value);
        AudioServer.SetBusVolumeDb(AudioBusNumber, decibal);
        TestSound.Play();
    }
    public void OnMasterValueChange(float _value)
    {
        AudioValueChange(MasterBusNumber, _value);
           
	}

    public void OnSFXValueChange(float _value)
	{
        AudioValueChange(SFXBusNumber, _value);
		

	}
	
	public void OnMusicValueChange(float _value)
	{
        AudioValueChange(MusicBusNumber, _value);
		
	}

    private float ConvertPercentageToDecibel(float _percentage) //Godot linear to decibal method missing int this version so this is a workaround
	{

		float scale = 20;
		float decibel = scale * (float)Math.Log10(_percentage);

		return decibel - 120;
    }

	private float ConvertDecibelToPercentage(float _decibel)
	{
		if (_decibel == -70)
		{
			return -70;
		}
        float scale = 5;
		float percentage = (float)Math.Pow(10, _decibel / scale);

        return _decibel;

    }

	public void OnResolutionButtonItemSelected(int i)
	{
        DisplayServer.WindowSetSize(Resolutions.Where(rs=>rs.Sres == ResolutionOptionButton.Text).FirstOrDefault().VIres);
	}

	class OBJResolutions
	{
		public string Sres;
		public Vector2I VIres;
	}

	class ObjWindowMode
	{
		public string sMode;
		public DisplayServer.WindowMode DsMode;
	}


}

