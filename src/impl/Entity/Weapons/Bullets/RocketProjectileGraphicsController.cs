using Godot;

public class RocketProjectileGraphicsController : GraphicsController
{
    public override void Update(GameObject gameObject)
    {
        gameObject.Rotation = (Mathf.Pi / 2) + gameObject.velocity.Angle();
    }

    public void PlayExplosionAnimation(GameObject gameObject)
    {
        var rocket = (RocketProjectile)gameObject;
        var sprite = rocket.GetNode<Sprite>("Sprite");
        sprite.Visible = false;
        AnimatedSprite animatedSprite = rocket.GetNode<AnimatedSprite>("AnimatedSprite");
        animatedSprite.Play();
    }
}