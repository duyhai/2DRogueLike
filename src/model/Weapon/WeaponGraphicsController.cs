using Godot;

public class WeaponGraphicsController : GraphicsController
{
    public override void Update(Node2D node)
    {
        Vector2 vector = node.GetGlobalMousePosition() - node.GlobalPosition;
        node.Rotation = vector.Angle();

        if (node.Scale.y > 0 ^ vector.Dot(Vector2.Right) > 0)
        {
            node.Scale = new Vector2(node.Scale.x, -node.Scale.y);
        }
    }
}