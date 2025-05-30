using Godot;
using Godot.Bridge;
using System;

public partial class SaveGame : Node
{
    private ConfigFile CF = new ConfigFile();
    private string CFName = "settings.cfg";


    public void SetVideoConfig()
    {
        CF.SetValue("Video", "Width",DisplayServer.WindowGetSize().X);
        CF.SetValue("Video", "Height",DisplayServer.WindowGetSize().Y);
        CF.SetValue("Video","WindowMode",DisplayServer.WindowGetMode().ToString());
    }

    public void SetAudioConfig()
    {
        CF.SetValue("Audio", "Master", AudioServer.GetBusVolumeDb(0));
        CF.SetValue("Audio", "SFX", AudioServer.GetBusVolumeDb(1));
        CF.SetValue("Audio", "Music", AudioServer.GetBusVolumeDb(2));
    }

    public void SaveConfig()
    {
        CF.Save("user://" + CFName);
    }

}
