using Godot;

public class WeaponGraphicsController : GraphicsController
{
    public override void Update(Node2D node)
    {
        Vector2 vector = node.GetGlobalMousePosition() - node.GlobalPosition;
        node.Rotation = vector.Angle();

        Sprite sprite = node.GetNode<Sprite>("Sprite");
        sprite.FlipV = vector.Dot(Vector2.Right) < 0;
    }
}