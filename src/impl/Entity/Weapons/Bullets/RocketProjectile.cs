using Godot;
using System.Diagnostics;

public class RocketProjectile : Bullet
{
    static int DAMAGE = 15;
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://RocketProjectile.tscn");
    Area2D explosionArea;

    public RocketProjectile() :
        base(new NullInputController(), new SimpleBulletPhysicsController(), new RocketProjectileGraphicsController(), DAMAGE)
    {
        speed = 350;
    }

    public override void _Ready()
    {
        explosionArea = GetNode<Area2D>("Area2D");
        explosionArea.CollisionLayer = CollisionLayer;
        explosionArea.CollisionMask = CollisionMask;
    }

    public override void HitTarget(KinematicCollision2D collision)
    {
        Explosion();
    }

    public void OnExplosionTimerTimeout()
    {
        Explosion();
    }

    public void OnAnimationSpriteExplosionFinished()
    {
        QueueFree();
    }

    private void Explosion()
    {
        velocity = Vector2.Zero;
        var bodies = explosionArea.GetOverlappingBodies();
        foreach (var body in bodies)
        {
            var method = body.GetType().GetMethod("Hit");
            method?.Invoke(body, new object[] { damage });
        }
        ((RocketProjectileGraphicsController)graphicsController).PlayExplosionAnimation(this);
    }
}
