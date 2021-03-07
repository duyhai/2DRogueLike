using Godot;

public class FlamethrowerFlameGraphicsController : GraphicsController
{
    public override void Update(Node2D node)
    {

    }

    public void PlayFadeAnimation(Node2D node)
    {
        AnimationPlayer animPlayer = node.GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
        animPlayer?.Play("Flame");
    }
}