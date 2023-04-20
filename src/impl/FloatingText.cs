using Godot;
using System;

public partial class FloatingText : Node2D
{
    public void ShowValue(string value, Vector2 travel, float duration, int spread)
    {
        Label label = GetNode<Label>("Label");
        label.Text = value;
        Random rnd = new Random();
        float angle = 2 * 3.14f * rnd.Next(-spread / 2, spread / 2) / 360;
        Vector2 movement = travel.Rotated(angle);
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(this, "position", Position + movement, duration).SetTrans(Tween.TransitionType.Linear).SetEase(Tween.EaseType.InOut);
        tween.TweenCallback(new Callable(this, "queue_free"));
        tween.Play();
    }
}
