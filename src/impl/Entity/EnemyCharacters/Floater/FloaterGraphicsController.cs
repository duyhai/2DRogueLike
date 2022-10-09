using System;
using Godot;

internal class FloaterGraphicsController : BasicGraphicsController
{
    public override void Update(Node2D node)
    {
        base.Update(node);

        GameObject gameObject = (GameObject)node;
        Player player = node.GetParent().GetNode<Player>("Player");
        AnimatedSprite animSprite = node.GetNode<AnimatedSprite>("AnimatedSprite");

        var direction = node.Position - player.Position;
        animSprite.FlipH = direction.x < 0;
    }
}