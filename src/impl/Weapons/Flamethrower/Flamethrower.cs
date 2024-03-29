using Godot;
using System;

public partial class Flamethrower : Weapon
{
    public Flamethrower() : base(new WeaponGraphicsController(), 0.33f) { }
    const int SPREAD = 25;

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = FlamethrowerFlame.SceneObject;
        weaponIconScene = GD.Load<PackedScene>("res://scenes/weapons/FlamethrowerIcon.tscn");
    }

    public override bool Shoot(Vector2 vector)
    {
        Random rnd = new Random();
        Vector2 rotatedVector = vector.Rotated(rnd.Next(-SPREAD, SPREAD + 1) / (float)180 * (float)Math.PI);
        return base.Shoot(rotatedVector);
    }
}
