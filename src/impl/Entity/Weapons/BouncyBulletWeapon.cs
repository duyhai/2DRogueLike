using Godot;

public class BouncyBulletWeapon : Weapon
{
    public BouncyBulletWeapon() : base(new WeaponGraphicsController()) { }

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = BouncyBullet.SceneObject;
    }

    public override bool Shoot(Vector2 vector, uint collisionLayer, uint collisionMask)
    {
        if (base.Shoot(vector, collisionLayer, collisionMask))
        {
            SoundManager.Instance.PlaySound(SoundPaths.BouncyWeapon);
            return true;
        }
        return false;
    }
}
