using Godot;
using System;

public class SoundPlayer : AudioStreamPlayer
{
    public void OnSoundPlayerFinished()
    {
        QueueFree();
    }
}
