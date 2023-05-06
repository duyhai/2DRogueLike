using Godot;

public partial class MeleeGraphicsController : WeaponGraphicsController
{
    public void Swing(Node2D node)
    {
        AnimationPlayer animationPlayer = node.GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Play("swing");
    }
}