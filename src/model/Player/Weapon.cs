using Godot;

public abstract class Weapon : Node2D
{
    protected int bulletSpeed;
    protected float weaponCooldown;
    protected Timer bulletTimer;
    protected PackedScene bulletScene;

    public virtual void Shoot(Vector2 vector)
    {
        if (bulletTimer.IsStopped())
        {
            bulletTimer.Start();
            var bullet = (Bullet)bulletScene.Instance();
            bullet.Initiate(vector.Angle(), Position);
            GetParent().AddChild(bullet);
        }
    }
}