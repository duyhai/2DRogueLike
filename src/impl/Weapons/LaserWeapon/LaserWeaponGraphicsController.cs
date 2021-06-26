using Godot;

public class LaserWeaponGraphicsController : WeaponGraphicsController
{
    public void appear(Node2D node)
    {
        Tween tween = node.GetNode<Tween>("Tip/RayCast2D/Tween");
        tween.StopAll();
        tween.InterpolateProperty(node.GetNode<Line2D>("Tip/RayCast2D/Line2D"), "width", 0, 10.0f, 0.2f);
        tween.Start();
    }

    public void disappear(Node2D node)
    {
        Tween tween = node.GetNode<Tween>("Tip/RayCast2D/Tween");
        tween.StopAll();
        tween.InterpolateProperty(node.GetNode<Line2D>("Tip/RayCast2D/Line2D"), "width", 10.0, 0, 0.1f);
        tween.Start();
    }
}