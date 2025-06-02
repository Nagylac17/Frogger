using Godot;
using Godot.Bridge;
using System;
using System.Xml.Linq;

public partial class LoadGame : Node
{
	private ConfigFile CF = new ConfigFile();
    private string CFName = "settings.cfg";


    public void LoadSave()
	{
        CF.Load("user://" + CFName);
        LoadVideoSettings();
        LoadAudioSettings();
    }

    private void LoadVideoSettings()
    {
        DisplayServer.WindowSetSize(new Vector2I((int)CF.GetValue("Video","Width"),(int)CF.GetValue("Video","Height")));
        DisplayServer.WindowSetPosition(new Vector2I(0, 0));
    }

    private void LoadAudioSettings()
    {
        GD.Print(AudioServer.GetBusVolumeDb(0));
        AudioServer.SetBusVolumeDb(0,(float)CF.GetValue("Audio","Master"));
        GD.Print(AudioServer.GetBusVolumeDb(0));
        AudioServer.SetBusVolumeDb(1,(float)CF.GetValue("Audio","SFX"));
        AudioServer.SetBusVolumeDb(2,(float)CF.GetValue("Audio","Music"));
        AudioServer.SetBusMute(0,(bool)CF.GetValue("Audio", "MasterMuted"));
        AudioServer.SetBusMute(1,(bool)CF.GetValue("Audio", "SFXMuted"));
        AudioServer.SetBusMute(2,(bool)CF.GetValue("Audio", "MusicMuted"));
    }


}
