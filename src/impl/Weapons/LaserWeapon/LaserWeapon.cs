using Godot;
using System;

public partial class LaserWeapon : Weapon
{
    public LaserWeapon() : base(new WeaponGraphicsController(), 0.33f) { }

    Timer triggerTimer;
    LaserBullet laserBullet;

    public override void _Ready()
    {
        triggerTimer = GetNode<Timer>("TriggerTimer");
        laserBullet = GetNode<LaserBullet>("Tip/LaserBullet");
        weaponIconScene = GD.Load<PackedScene>("res://scenes/weapons/LaserWeaponIcon.tscn");
        GameObject initiator = GetParent<GameObject>();
        laserBullet.Initiate(initiator, 0, laserBullet.Position, (int)(initiator.Stats.Damage * damageMultiplier));
        bulletScene = LaserBullet.SceneObject;
    }

    public override bool Shoot(Vector2 vector)
    {
        laserBullet.SetIsCasting(true);
        triggerTimer.Start();
        return true;
    }

    public void OnTriggerTimerTimeout()
    {
        laserBullet.SetIsCasting(false);
    }
}
