using Godot;
using System;

public class FloatingText : Node2D
{
    Random rnd = new Random();
    Vector2 movement;
    Tween tween;
    Label label;

    public void ShowValue(string value, Vector2 travel, float duration, int spread)
    {
        label.Text = value;
        float angle = 2 * 3.14f * rnd.Next(-spread / 2, spread / 2) / 360;
        movement = travel.Rotated(angle);
        tween.InterpolateProperty(this, "position", Position, Position + movement, duration, Tween.TransitionType.Linear, Tween.EaseType.InOut);
        tween.Start();
    }

    public override void _Ready()
    {
        label = GetNode<Label>("Label");
        tween = GetNode<Tween>("Tween");
    }
    
    public void OnTweenAllCompleted()
    {
        QueueFree();
    }
}
