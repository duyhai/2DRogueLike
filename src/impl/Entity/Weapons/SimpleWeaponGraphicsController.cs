using Godot;

public class SimpleWeaponGraphicsController : WeaponGraphicsController
{
    public void MuzzleFlash(Node2D node)
    {
        AnimationPlayer animationPlayer = node.GetNode<AnimationPlayer>("Sprite/AnimationPlayer");
        animationPlayer.Play("muzzleflash");
    }
}