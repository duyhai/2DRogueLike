using Godot;

public class BouncyBulletWeapon : Weapon
{
    public BouncyBulletWeapon() : base(new WeaponGraphicsController(), 2f) { }

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = BouncyBullet.SceneObject;
        weaponIconScene = GD.Load<PackedScene>("res://scenes/weapons/BouncyBulletWeaponIcon.tscn");
    }

    public override bool Shoot(Vector2 vector)
    {
        if (base.Shoot(vector))
        {
            SoundManager.Instance.PlaySound(SoundPaths.BouncyWeapon);
            return true;
        }
        return false;
    }
}
