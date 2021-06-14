using Godot;

public class RocketLauncher : Weapon
{
    public RocketLauncher() : base(new WeaponGraphicsController(), 15) { }

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = RocketProjectile.SceneObject;
    }

    public override bool Shoot(Vector2 vector, uint collisionLayer, uint collisionMask)
    {
        if (base.Shoot(vector, collisionLayer, collisionMask))
        {
            SoundManager.Instance.PlaySound(SoundPaths.RocketLaunching);
            return true;
        }
        return false;
    }
}
