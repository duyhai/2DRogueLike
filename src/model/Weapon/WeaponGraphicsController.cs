using Godot;

public partial class WeaponGraphicsController : GraphicsController
{
    public override void Update(Node2D node)
    {
        Player player = node.GetParent<Player>();
        Vector2 vector = player.ViewDirection;
        node.Rotation = vector.Angle();

        if (node.Scale.Y > 0 ^ vector.Dot(Vector2.Right) > 0)
        {
            node.Scale = new Vector2(node.Scale.X, -node.Scale.Y);
        }
    }
}