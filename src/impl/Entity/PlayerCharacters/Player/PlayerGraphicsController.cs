using Godot;

public class PlayerGraphicsController : BasicGraphicsController
{
    public override void Update(Node2D node)
    {
        base.Update(node);

        AnimatedSprite animSprite = node.GetNode<AnimatedSprite>("AnimatedSprite");

        Vector2 vector = node.GetGlobalMousePosition() - node.GlobalPosition;
        animSprite.FlipH = vector.x > 0;
        animSprite.Animation = "walk";
        animSprite.Playing = true;
    }
}