using Godot;
using System;

public partial class BasicGraphicsController : GraphicsController
{
    public override void Update(Node2D node)
    {
        AnimatedSprite2D animSprite = node.GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        GameObject gameObject = (GameObject)node;
        if (gameObject.isDead)
        {
            return;
        }

        bool moving = gameObject.Velocity.Length() > 0.0001;
        bool goingRight = (gameObject.Velocity.X > 0) || (!moving && animSprite.FlipH);
        animSprite.FlipH = goingRight;

        bool goingUp = gameObject.Velocity.Y < 0;
        if (moving)
        {
            string animationName = goingUp ? "walkUp" : "walk";
            animSprite.Play(animationName);
        }
        else
        {
            if (Array.IndexOf(animSprite.SpriteFrames.GetAnimationNames(), "idle") != -1)
            {
                animSprite.Play("idle");
            }
            else
            {
                animSprite.Stop();
                animSprite.Frame = 0;
            }
        }
    }

    public void ResetSprite(GameObject node)
    {
        AnimatedSprite2D animSprite = node.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animSprite.Modulate = new Color(1, 1, 1, 1);
    }

    public void PlayDeathAnimation(GameObject node)
    {
        AnimatedSprite2D animSprite = node.GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D");
        if (Array.IndexOf(animSprite?.SpriteFrames.GetAnimationNames(), "death") != -1)
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