using Godot;
using System;

public class SoundManager : Node2D
{
    public static SoundManager Instance { get; set; }
    public Node SoundContainer { get; set; }
    private readonly PackedScene soundPlayerScene = (PackedScene)GD.Load("res://scenes/Sound/SoundPlayer.tscn");
    private readonly PackedScene soundPlayer2DScene = (PackedScene)GD.Load("res://scenes/Sound/SoundPlayer2D.tscn");

    public override void _Ready()
    {
        Instance = this;
    }

    public void StopAllSound()
    {
        Node soundsNode = GetNode<Node2D>("Main/Sounds");
        foreach (Node i in soundsNode.GetChildren())
        {
            i.QueueFree();
        }
    }

    public SoundPlayer PlaySound(string soundPath, bool loop = false)
    {
        AudioStreamSample audioStream = GD.Load<AudioStreamSample>(soundPath);
        SoundPlayer soundPlayer = (SoundPlayer)soundPlayerScene.Instance();
        soundPlayer.Stream = audioStream;
        soundPlayer.Loop = loop;
        SoundContainer.AddChild(soundPlayer);
        soundPlayer.Play();
        return soundPlayer;
    }

    public SoundPlayer2D PlaySound(string soundPath, Vector2 position, bool loop = false)
    {
        AudioStreamSample audioStream = GD.Load<AudioStreamSample>(soundPath);
        SoundPlayer2D soundPlayer2D = (SoundPlayer2D)soundPlayer2DScene.Instance();
        soundPlayer2D.Position = position;
        soundPlayer2D.Stream = audioStream;
        soundPlayer2D.Loop = loop;
        SoundContainer.AddChild(soundPlayer2D);
        soundPlayer2D.Play();
        return soundPlayer2D;
    }
}