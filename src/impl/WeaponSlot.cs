using Godot;
using System;

public partial class WeaponSlot : MarginContainer
{
    public static PackedScene Scene = (PackedScene)GD.Load("res://scenes/hud/WeaponSlot.tscn");
    private TextureRect frameActive = null;
    private TextureRect frameInactive = null;

    public Boolean Active
    {
        set
        {
            if (frameActive != null)
            {
                frameActive.Visible = value;
                frameInactive.Visible = !value;
            }
        }
        get
        {
            if (frameActive != null)
            {
                return frameActive.Visible;
            }
            else
            {
                return false;
            }
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        frameActive = GetNode<TextureRect>("FrameActive");
        frameInactive = GetNode<TextureRect>("FrameInactive");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void AddWeaponIcon(TextureRect icon)
    {
        GetNode<MarginContainer>("MarginContainer").AddChild(icon);
    }
}
