using Godot;
using System;

public class SoundManager : Node2D
{
    public static SoundManager Instance { get; set; }
    public Node SoundContainer { get; set; }
    private readonly PackedScene soundPlayerScene = (PackedScene)GD.Load("SoundPlayer.tscn");
    private readonly PackedScene soundPlayer2DScene = (PackedScene)GD.Load("SoundPlayer2D.tscn");

    public override void _Ready()
    {
        Instance = this;
    }

    public void PlaySound(string soundPath, Vector2? position = null)
    {
        AudioStream audioStream = (AudioStreamSample)GD.Load(soundPath);
        if (position != null)
        {
            SoundPlayer2D soundPlayer2D = (SoundPlayer2D)soundPlayer2DScene.Instance();
            soundPlayer2D.Position = (Vector2)position;
            soundPlayer2D.Stream = audioStream;
            SoundContainer.AddChild(soundPlayer2D);
            soundPlayer2D.Play();
        }
        else
        {
            SoundPlayer soundPlayer = (SoundPlayer)soundPlayerScene.Instance();
            soundPlayer.Stream = audioStream;
            SoundContainer.AddChild(soundPlayer);
            soundPlayer.Play();
        }
    }
}