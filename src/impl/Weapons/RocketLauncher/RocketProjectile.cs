using Godot;
using System.Diagnostics;

public class RocketProjectile : Bullet
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/weapons/projectiles/RocketProjectile.tscn");
    bool exploded = false;

    public RocketProjectile() :
        base(new NullInputController(), new SimpleBulletPhysicsController(), new NullGraphicsController())
    {
        baseStats = new StatsInfo { MaxHealth = 0, MaxShield = 0, Damage = 0, Speed = 350 };
    }

    public override void _Ready()
    {
        Rotation = (Mathf.Pi / 2) + velocity.Angle();
    }

    public override int HitTarget(Node collider)
    {
        if (!exploded)
        {
            Explosion();
            GetNode<Timer>("ExplosionTimer").Stop();
        }
        return 0;
    }

    public void OnExplosionTimerTimeout()
    {
        Explosion();
    }

    private void Explosion()
    {
        exploded = true;
        SoundManager.Instance.PlaySound(SoundPaths.Explosion, Position);

        RocketExplosion rocketExplosion = (RocketExplosion)RocketExplosion.SceneObject.Instance();
        rocketExplosion.Initiate(initiator, Position, damage);
        rocketExplosion.CollisionLayer = CollisionLayer;
        rocketExplosion.CollisionMask = CollisionMask;
        GetParent().AddChild(rocketExplosion);

        velocity = Vector2.Zero;
        QueueFree();
    }
}
