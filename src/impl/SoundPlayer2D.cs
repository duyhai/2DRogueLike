using Godot;
using System;

public class SoundPlayer2D : AudioStreamPlayer2D
{
    public void OnSoundPlayer2DFinished()
    {
        QueueFree();
    }
}
