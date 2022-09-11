using Godot;
using System;

public class FreezingWeapon : Weapon
{
    public FreezingWeapon() : base(new WeaponGraphicsController(), 0f) { }
    const int SPREAD = 2;

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = FreezingBullet.SceneObject;
        weaponIconScene = GD.Load<PackedScene>("res://scenes/weapons/FreezingWeaponIcon.tscn");
    }

    public override bool Shoot(Vector2 vector)
    {
        Random rnd = new Random();
        Vector2 rotatedVector = vector.Rotated(rnd.Next(-SPREAD, SPREAD + 1) / (float)180 * (float)Math.PI);
        return base.Shoot(rotatedVector);
    }
}