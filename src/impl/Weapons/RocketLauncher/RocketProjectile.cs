using Godot;
using System.Diagnostics;

public class RocketProjectile : Bullet
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/weapons/projectiles/RocketProjectile.tscn");
    Area2D explosionArea;
    bool exploded = false;

    public RocketProjectile() :
        base(new NullInputController(), new SimpleBulletPhysicsController(), new RocketProjectileGraphicsController())
    {
        baseSpeed = 350;
    }

    public override void _Ready()
    {
        explosionArea = GetNode<Area2D>("Area2D");
        explosionArea.CollisionLayer = CollisionLayer;
        explosionArea.CollisionMask = CollisionMask;
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

    public void OnAnimationSpriteExplosionFinished()
    {
        QueueFree();
    }

    private int Explosion()
    {
        exploded = true;
        SoundManager.Instance.PlaySound(SoundPaths.Explosion, Position);
        velocity = Vector2.Zero;

        var bodies = explosionArea.GetOverlappingBodies();
        foreach (Node2D body in bodies)
        {
            var bullet = (Bullet)PhantomBullet.SceneObject.Instance();
            bullet.Initiate(initiator, 0, body.GlobalPosition, damage);
            bullet.CollisionLayer = CollisionLayer;
            bullet.CollisionMask = CollisionMask;
            GetParent().AddChild(bullet);
        }

        ((RocketProjectileGraphicsController)graphicsController).PlayExplosionAnimation(this);
        return 0; // Nem tudom mit csináljak ezzel
    }
}
