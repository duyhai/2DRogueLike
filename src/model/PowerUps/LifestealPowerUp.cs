using Godot;
using System;

public class LifestealPowerUp : PowerUp
{
    protected float percentage = 0.5f;
    public float Percentage { get => percentage; }
    public override void Effect() { }
}
