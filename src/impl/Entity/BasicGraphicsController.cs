using Godot;

public class BasicGraphicsController : GraphicsController
{
    public override void Update(Node2D node)
    {
        AnimatedSprite animSprite = node.GetNode<AnimatedSprite>("AnimatedSprite");

        GameObject gameObject = (GameObject)node;

        bool goingLeft = gameObject.velocity.x < 0;
        animSprite.FlipH = goingLeft;

        int frame = animSprite.Frame;
        bool goingUp = gameObject.velocity.y < 0;
        animSprite.Animation = goingUp ? "walkUp" : "walk";
        bool moving = gameObject.velocity.Length() > 0.0001;
        if (moving)
        {
            animSprite.Frame = frame;
            System.Console.WriteLine(frame);
        }
        animSprite.Playing = moving;
    }

    public void ResetSprite(GameObject node)
    {
        AnimatedSprite animSprite = node.GetNode<AnimatedSprite>("AnimatedSprite");
        animSprite.Modulate = new Color(1, 1, 1, 1);
    }

    public void PlayDeathAnimation(GameObject node)
    {
        AnimationPlayer animPlayer = node.GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
        animPlayer?.Play("death");
    }

    public void PlayHitAnimation(GameObject node)
    {
        AnimationPlayer animPlayer = node.GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
        animPlayer?.Play("hit");
    }
}