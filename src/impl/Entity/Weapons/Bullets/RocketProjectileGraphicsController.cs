using Godot;

public class RocketProjectileGraphicsController : GraphicsController
{
    public override void Update(Node2D node)
    {
        var rocket = (RocketProjectile)node;
        rocket.Rotation = (Mathf.Pi / 2) + rocket.velocity.Angle();
    }

    public void PlayExplosionAnimation(Node2D node)
    {
        var rocket = (RocketProjectile)node;
        var sprite = rocket.GetNode<Sprite>("Sprite");
        sprite.Visible = false;
        var fire = rocket.GetNode<Node2D>("Fire");
        fire.Visible = false;
        AnimatedSprite animatedSprite = rocket.GetNode<AnimatedSprite>("AnimatedSprite");
        animatedSprite.Play();
    }
}