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

            bullet.Initiate(vector.Angle(), ((Node2D)GetParent()).GlobalPosition);
            bullet.CollisionLayer = collisionLayer;
            bullet.CollisionMask = collisionMask;
            GetParent().GetParent().AddChild(bullet);
        }
    }

    public void SetWeaponCooldown(float weaponCooldown)
    {
        bulletTimer.WaitTime = weaponCooldown;
    }
}