using Godot;

public class Lightning : Line2D
{
    public void PlayAnimation()
    {
        AnimationPlayer animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Play("lightning");
    }

    public void OnAnimationPlayerAnimationFinished(string animationName)
    {
        QueueFree();
    }
}
