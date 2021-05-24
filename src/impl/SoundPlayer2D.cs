using Godot;
using System;

public class SoundPlayer2D : AudioStreamPlayer2D
{
    public bool Loop = false;

    public void OnSoundPlayer2DFinished()
    {
        if (!Loop)
        {
            QueueFree();
        }
    }
}
