using Godot;
using Godot.Collections;
using System.Linq;

public partial class BallLightningWeaponV2 : Weapon
{
    public BallLightningWeaponV2() : base(new WeaponGraphicsController(), 0.5f) { }

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = BallLightningBulletV2.SceneObject;
        weaponIconScene = GD.Load<PackedScene>("res://scenes/weapons/BallLightningWeaponIcon.tscn");
    }
}
