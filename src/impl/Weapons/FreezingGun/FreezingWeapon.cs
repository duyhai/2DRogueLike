using Godot;
using System;

public class FreezingWeapon : Weapon
{
    public FreezingWeapon() : base(new WeaponGraphicsController(), 0.1f) { }
    const int SPREAD = 2;

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = FreezingBullet.SceneObject;
    }

    public override bool Shoot(Vector2 vector, uint collisionLayer, uint collisionMask)
    {
        Random rnd = new Random();
        Vector2 rotatedVector = vector.Rotated(rnd.Next(-SPREAD, SPREAD + 1) / (float)180 * (float)Math.PI);
        return base.Shoot(rotatedVector, collisionLayer, collisionMask);
    }
}