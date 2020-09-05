using Godot;

public abstract class Weapon : Node2D
{
    protected int bulletSpeed;
    public float weaponCooldown;
    protected Timer bulletTimer;
    protected PackedScene bulletScene;

    public virtual void Shoot(Vector2 vector, uint collisionLayer, uint collisionMask)
    {
        if (bulletTimer.IsStopped())
        {
            bulletTimer.Start();
            var bullet = (Bullet)bulletScene.Instance();
            bullet.Initiate(vector.Angle(), Position);
            bullet.CollisionLayer = collisionLayer;
            bullet.CollisionMask = collisionMask;
            GetParent().AddChild(bullet);
        }
    }

    public void SetWeaponCooldown(float weaponCooldown)
    {
        bulletTimer.WaitTime = weaponCooldown;
    }
}