using Godot;

public partial class SimpleWeapon : Weapon
{

    public SimpleWeapon() : base(new SimpleWeaponGraphicsController(), 2f) { }

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = SimpleBullet.SceneObject;
        weaponIconScene = GD.Load<PackedScene>("res://scenes/weapons/SimpleWeaponIcon.tscn");
    }

    public override bool Shoot(Vector2 vector)
    {
        if (base.Shoot(vector))
        {
            ((SimpleWeaponGraphicsController)graphicsController).MuzzleFlash(this);
            SoundManager.Instance.PlaySound(SoundPaths.Gunshot);
            return true;
        }
        return false;
    }
}
