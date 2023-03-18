using Godot;

public class LaserBulletGraphicsController : GraphicsController
{
    SceneTreeTween tween = null;

    public override void Update(Node2D node) { }

    public void Appear(Node2D node)
    {
        var laser = node.GetNode<Line2D>("RayCast2D/Line2D");
        tween?.Kill();
        tween = node.GetTree().CreateTween();
        tween.TweenProperty(laser, "width", 10.0f, 0.2f);
        tween.Play();
    }

    public void Disappear(Node2D node)
    {
        var laser = node.GetNode<Line2D>("RayCast2D/Line2D");
        tween?.Kill();
        tween = node.GetTree().CreateTween();
        tween.TweenProperty(laser, "width", 0f, 0.1f);
        tween.Play();
    }
}