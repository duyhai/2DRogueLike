using Godot;
using System;

public class BasicGraphicsController : GraphicsController
{
    public override void Update(Node2D node)
    {
        AnimatedSprite animSprite = node.GetNode<AnimatedSprite>("AnimatedSprite");

        GameObject gameObject = (GameObject)node;
        if (gameObject.isDead)
        {
            return;
        }

        bool moving = gameObject.velocity.Length() > 0.0001;
        bool goingRight = (gameObject.velocity.x > 0) || (!moving && animSprite.FlipH);
        animSprite.FlipH = goingRight;

        int frame = animSprite.Frame;
        bool goingUp = gameObject.velocity.y < 0;
        if (moving)
        {
            animSprite.Frame = frame;
            animSprite.Animation = goingUp ? "walkUp" : "walk";
            animSprite.Playing = true;
        }
        else
        {
            if (Array.IndexOf(animSprite.Frames.GetAnimationNames(), "idle") != -1)
            {
                animSprite.Animation = "idle";
                animSprite.Playing = true;
            }
            else
            {
                animSprite.Playing = false;
                animSprite.Frame = 0;
            }
        }
    }

    public void ResetSprite(GameObject node)
    {
        AnimatedSprite animSprite = node.GetNode<AnimatedSprite>("AnimatedSprite");
        animSprite.Modulate = new Color(1, 1, 1, 1);
    }

    public void PlayDeathAnimation(GameObject node)
    {
        AnimatedSprite animSprite = node.GetNodeOrNull<AnimatedSprite>("AnimatedSprite");
        if (Array.IndexOf(animSprite?.Frames.GetAnimationNames(), "death") != -1)
        {
            animSprite.Play("death");
        }
        else
        {
            PlayFadeAnimation(node);
        }
    }

    public void PlayFadeAnimation(GameObject node)
    {
        AnimationPlayer animPlayer = node.GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
        animPlayer?.Play("death");
    }

    public virtual void PlayHitAnimation(GameObject node)
    {
        AnimationPlayer animPlayer = node.GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
        animPlayer?.Play("hit");
    }
}