using Godot;
using System;

public class Flamethrower : Weapon
{
    public Flamethrower() : base(new WeaponGraphicsController(), 0.33f) { }

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = FlamethrowerFlame.SceneObject;
    }

    public override bool Shoot(Vector2 vector, uint collisionLayer, uint collisionMask)
    {
        Random rnd = new Random();
        Vector2 rotatedVector = vector.Rotated(rnd.Next(-25, 26) / (float)180 * (float)Math.PI);
        return base.Shoot(rotatedVector, collisionLayer, collisionMask);
    }
}
