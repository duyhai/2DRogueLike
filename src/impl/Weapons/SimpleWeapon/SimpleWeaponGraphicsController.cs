using Godot;

public partial class SimpleWeaponGraphicsController : WeaponGraphicsController
{
    public void MuzzleFlash(Node2D node)
    {
        AnimationPlayer animationPlayer = node.GetNode<AnimationPlayer>("Sprite2D/AnimationPlayer");
        animationPlayer.Play("muzzleflash");
    }
}