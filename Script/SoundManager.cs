using Godot;
using System;
using System.Collections.Generic;

public partial class SoundManager : Node2D
{

    static AudioStreamPlayer2D audioPlayer;
	[Export]
    public Godot.Collections.Dictionary<string,AudioStreamWav> DeathAudio { get; set; }

	static public string deathType;

    public override void _Ready()
	{
		audioPlayer = GetNode<AudioStreamPlayer2D>("Audio");
	}

	public void PlaySound(string deathType)
	{
		audioPlayer.Stream = DeathAudio[deathType];
		audioPlayer.Play();
	}

	public void AddToMain(Node2D MainNode)
	{
            MainNode.AddChild(this);
    }
		



}
