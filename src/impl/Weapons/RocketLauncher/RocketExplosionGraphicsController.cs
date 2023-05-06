using Godot;

public partial class RocketExplosionGraphicsController : GraphicsController
{
    public override void Update(Node2D node) { }

    public void PlayExplosionAnimation(Node2D node)
    {
        AnimatedSprite2D animatedSprite = node.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animatedSprite.Play();
    }
}