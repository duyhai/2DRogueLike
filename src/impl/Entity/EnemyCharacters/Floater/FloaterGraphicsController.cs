using System;
using Godot;

internal class FloaterGraphicsController : BasicGraphicsController
{
    public override void Update(Node2D node)
    {
        base.Update(node);

        GameObject gameObject = (GameObject)node;
        Player player = node.GetParent().GetNode<Player>("Player");
        AnimatedSprite2D animSprite = node.GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        var direction = node.Position - player.Position;
        animSprite.FlipH = direction.X < 0;
    }
}