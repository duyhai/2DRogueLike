using Godot;

public class LaserBulletGraphicsController : GraphicsController
{
    public override void Update(Node2D node) { }

    public void Appear(Node2D node)
    {
        Tween tween = node.GetNode<Tween>("RayCast2D/Tween");
        tween.StopAll();
        tween.InterpolateProperty(node.GetNode<Line2D>("RayCast2D/Line2D"), "width", 0, 10.0f, 0.2f);
        tween.Start();
    }

    public void Disappear(Node2D node)
    {
        Tween tween = node.GetNode<Tween>("RayCast2D/Tween");
        tween.RemoveAll();
        tween.InterpolateProperty(node.GetNode<Line2D>("RayCast2D/Line2D"), "width", 10.0f, 0, 0.1f);
        tween.Start();
    }
}