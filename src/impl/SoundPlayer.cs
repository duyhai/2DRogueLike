using Godot;
using System;

public class SoundPlayer : AudioStreamPlayer
{
    public bool Loop = false;

    public void OnSoundPlayerFinished()
    {
        if (!Loop)
        {
            QueueFree();
        }
    }
}
