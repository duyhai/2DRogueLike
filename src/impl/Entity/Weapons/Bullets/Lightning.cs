using Godot;
using System;

public class Lightning : Line2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public void PlayAnimation()
    {
        AnimationPlayer animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Play("lightning");
    }

    public void OnAnimationPlayerAnimationFinished(string animationName)
    {
        QueueFree();
    }
}
