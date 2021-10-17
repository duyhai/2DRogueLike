using Godot;
using Godot.Collections;
using System;

public class Bar : HBoxContainer
{
    PackedScene dividerScene = GD.Load<PackedScene>("res://scenes/hud/BarDivider.tscn");
    const int DIVIDER_CONTAINER_SIZE = 376;
    const int DIVIDER_SIZE = 2;
    const int VALUE_PER_DIVIDER = 250;
    public override void _Ready()
    {
    }

    public void SetMaxValue(int value)
    {
        // If MinValue == MaxValue (MinValue == 0), the editor gives runtime errors
        GetNode<TextureProgress>("TextureProgress").MaxValue = Math.Max(1, value);

        HBoxContainer dividerContainer = GetNode<HBoxContainer>("TextureProgress/Dividers");

        float numberOfBlocks = value / (float)VALUE_PER_DIVIDER;
        int numberOfDividersNeeded = Math.Min((int)(numberOfBlocks + 1), DIVIDER_CONTAINER_SIZE / DIVIDER_SIZE);
        int availableBlockSpace = DIVIDER_CONTAINER_SIZE - (numberOfDividersNeeded * DIVIDER_SIZE);
        double blockWidth = Math.Round(availableBlockSpace / numberOfBlocks);
        dividerContainer.Set("custom_constants/separation", blockWidth);
        Godot.Collections.Array dividers = dividerContainer.GetChildren();

        // Add more divider if needed
        for (int i = 0; i < numberOfDividersNeeded - dividers.Count; i++)
        {
            TextureRect divider = (TextureRect)dividerScene.Instance();
            dividerContainer.AddChild(divider);
        }

        // Remove dividers if needed
        for (int i = 0; i < dividers.Count - numberOfDividersNeeded; i++)
        {
            ((TextureRect)dividers[i]).Free();
        }
    }

    public void SetValue(int value)
    {
        GetNode<TextureProgress>("TextureProgress").Value = value;
    }

    public override void _Process(float delta)
    {

    }
}
