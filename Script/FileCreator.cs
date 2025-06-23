using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class FileCreator
    {
        private static ConfigFile SC = new ConfigFile();
        private static ConfigFile CF = new ConfigFile();
        private  static string SCName = "Score.cfg";
        private  static string CFName = "settings.cfg";

    public static void CreateCFGFile()
    {
        if (CF.Load("user://" + CFName) != Godot.Error.Ok)
        {
            SetVideoConfig();
            SetAudioConfig();
            CF.Save("user://" + CFName);
        }
        
    }

    private static void SetVideoConfig()
    {
        CF.SetValue("Video", "Width", DisplayServer.ScreenGetSize().X);
        CF.SetValue("Video", "Height", DisplayServer.ScreenGetSize().Y);
        CF.SetValue("Video", "WindowMode", DisplayServer.WindowMode.Fullscreen.ToString());

    }

    private static void SetAudioConfig()
    {
        CF.SetValue("Audio", "Master", AudioServer.GetBusVolumeDb(0));
        CF.SetValue("Audio", "SFX", AudioServer.GetBusVolumeDb(1));
        CF.SetValue("Audio", "Music", AudioServer.GetBusVolumeDb(2));
        CF.SetValue("Audio", "MasterMuted", AudioServer.IsBusMute(0));
        CF.SetValue("Audio", "SFXMuted", AudioServer.IsBusMute(1));
        CF.SetValue("Audio", "MusicMuted", AudioServer.IsBusMute(2));
    }

    public static void CreateHighScoreFile()
    {
        if (SC.Load("user://" + SCName) != Godot.Error.Ok)
        { 
         SetHighScore();
         SC.Save("user://" + SCName);
        }
    }

    private static void SetHighScore()
    {
        SC.SetValue("HighScore", "Score", 0);
    }

    }

