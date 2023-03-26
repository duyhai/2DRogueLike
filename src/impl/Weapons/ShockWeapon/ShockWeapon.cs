using Godot;
using Godot.Collections;
using System.Linq;

public partial class ShockWeapon : Weapon
{
    public ShockWeapon() : base(new ShockWeaponGraphicsController(), 4f) { }

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = LightningBullet.SceneObject;
        weaponIconScene = GD.Load<PackedScene>("res://scenes/weapons/ShockWeaponIcon.tscn");
    }

    public override bool Shoot(Vector2 vector)
    {
        if (base.Shoot(vector))
        {
            SoundManager.Instance.PlaySound(SoundPaths.Lightning);
            return true;
        }

        return false;
    }
}
