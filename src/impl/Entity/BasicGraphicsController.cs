using Godot;

public class BasicGraphicsController : GraphicsController
{
    public override void Update(GameObject gameObject)
    {
        AnimatedSprite animSprite = gameObject.GetNode<AnimatedSprite>("AnimatedSprite");

        bool goingLeft = gameObject.velocity.x < 0;
        animSprite.FlipH = goingLeft;

        bool goingUp = gameObject.velocity.y < 0;
        animSprite.Animation = goingUp ? "walkUp" : "walk";

        bool moving = gameObject.velocity.Length() > 0.0001;
        animSprite.Playing = moving;
    }

    public void ResetSprite(GameObject gameObject)
    {
        AnimatedSprite animSprite = gameObject.GetNode<AnimatedSprite>("AnimatedSprite");
        animSprite.Modulate = new Color(1, 1, 1, 1);
    }

    public void PlayDeathAnimation(GameObject gameObject)
    {
        AnimationPlayer animPlayer = gameObject.GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
        animPlayer?.Play("death");
    }

    public void PlayHitAnimation(GameObject gameObject)
    {
        AnimationPlayer animPlayer = gameObject.GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
        animPlayer?.Play("hit");
    }
}