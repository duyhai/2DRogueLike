using Godot;

public class WeaponGraphicsController : GraphicsController
{
    public override void Update(Node2D node)
    {
        Vector2 vector = node.GetGlobalMousePosition() - node.GlobalPosition;
        node.Rotation = vector.Angle();

        Sprite sprite = node.GetNode<Sprite>("Sprite");
        if (sprite.Scale.y > 0 ^ vector.Dot(Vector2.Right) > 0)
        {
            sprite.Scale = new Vector2(sprite.Scale.x, -sprite.Scale.y);
        }
    }
}