using Godot;

public partial class RocketLauncher : Weapon
{
    public RocketLauncher() : base(new WeaponGraphicsController(), 1.5f) { }

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = RocketProjectile.SceneObject;
        weaponIconScene = GD.Load<PackedScene>("res://scenes/weapons/RocketProjectileIcon.tscn");
    }

    public override bool Shoot(Vector2 vector)
    {
        if (base.Shoot(vector))
        {
            SoundManager.Instance.PlaySound(SoundPaths.RocketLaunching);
            return true;
        }
        return false;
    }
}
