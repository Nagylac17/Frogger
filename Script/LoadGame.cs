using Godot;
using Godot.Bridge;
using System;
using System.Xml.Linq;
using static Godot.DisplayServer;

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

    private DisplayServer.WindowMode SetWindowMode()
    {
        var e = (string)CF.GetValue("Video", "WindowMode");
        var b = DisplayServer.WindowMode.Windowed.ToString();
        if ((string)CF.GetValue("Video", "WindowMode") == DisplayServer.WindowMode.Windowed.ToString())
        {
            return DisplayServer.WindowMode.Windowed;
        }
        else
        {
            return DisplayServer.WindowMode.Fullscreen;
        }
    }

    private void LoadVideoSettings()
    {
        DisplayServer.WindowSetSize(new Vector2I((int)CF.GetValue("Video","Width"),(int)CF.GetValue("Video","Height")));        
        DisplayServer.WindowSetMode(SetWindowMode());
    }

    private void LoadAudioSettings()
    {
        AudioServer.SetBusVolumeDb(0,(float)CF.GetValue("Audio","Master"));
        AudioServer.SetBusVolumeDb(1,(float)CF.GetValue("Audio","SFX"));
        AudioServer.SetBusVolumeDb(2,(float)CF.GetValue("Audio","Music"));
        AudioServer.SetBusMute(0,(bool)CF.GetValue("Audio", "MasterMuted"));
        AudioServer.SetBusMute(1,(bool)CF.GetValue("Audio", "SFXMuted"));
        AudioServer.SetBusMute(2,(bool)CF.GetValue("Audio", "MusicMuted"));
    }


}
