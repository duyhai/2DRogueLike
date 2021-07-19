using Godot;
using System.Diagnostics;

public class RocketProjectile : Bullet
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/weapons/projectiles/RocketProjectile.tscn");
    Area2D explosionArea;
    bool exploded = false;

    public RocketProjectile() :
        base(new NullInputController(), new SimpleBulletPhysicsController(), new NullGraphicsController())
    {
        baseSpeed = 350;
    }

    public override void _Ready()
    {
        Rotation = (Mathf.Pi / 2) + velocity.Angle();
    }

    public override int HitTarget(Node collider)
    {
        int inflictedDamage = 0;
        if (!exploded)
        {
            inflictedDamage = Explosion();
            GetNode<Timer>("ExplosionTimer").Stop();
        }
        return inflictedDamage;
    }

    public void OnExplosionTimerTimeout()
    {
        Explosion();
    }

    private int Explosion()
    {
        exploded = true;
        SoundManager.Instance.PlaySound(SoundPaths.Explosion, Position);

        RocketHitBullet rocketHitBullet = (RocketHitBullet)RocketHitBullet.SceneObject.Instance();
        rocketHitBullet.Initiate(initiator, 0, Position, damage);
        rocketHitBullet.CollisionLayer = CollisionLayer;
        rocketHitBullet.CollisionMask = CollisionMask;
        GetParent().AddChild(rocketHitBullet);

        velocity = Vector2.Zero;
        QueueFree();
        return 0;
    }
}
