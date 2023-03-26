using Godot;
using Godot.Collections;
using System;

public partial class Bar : HBoxContainer
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
        GetNode<TextureProgressBar>("TextureProgressBar").MaxValue = Math.Max(1, value);

        HBoxContainer dividerContainer = GetNode<HBoxContainer>("TextureProgressBar/Dividers");

        float numberOfBlocks = value / (float)VALUE_PER_DIVIDER;
        int numberOfDividersNeeded = Math.Min((int)(numberOfBlocks + 1), DIVIDER_CONTAINER_SIZE / DIVIDER_SIZE);
        int availableBlockSpace = DIVIDER_CONTAINER_SIZE - (numberOfDividersNeeded * DIVIDER_SIZE);
        double blockWidth = Math.Round(availableBlockSpace / numberOfBlocks);
        dividerContainer.Set("theme_override_constants/separation", blockWidth);
        var dividers = dividerContainer.GetChildren();

        // Add more divider if needed
        for (int i = 0; i < numberOfDividersNeeded - dividers.Count; i++)
        {
            TextureRect divider = (TextureRect)dividerScene.Instantiate();
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
        GetNode<TextureProgressBar>("TextureProgressBar").Value = value;
    }
}
