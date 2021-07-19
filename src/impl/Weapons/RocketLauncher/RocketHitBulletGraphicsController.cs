using Godot;

public class RocketHitBulletGraphicsController : GraphicsController
{
    public override void Update(Node2D node) { }

    public void PlayExplosionAnimation(Node2D node)
    {
        AnimatedSprite animatedSprite = node.GetNode<AnimatedSprite>("AnimatedSprite");
        animatedSprite.Play();
    }
}