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

        int inflictedDamage = 0;
        float lifestealPercentage = 0f;
        if (initiator.IsInsideTree())
        {
            var powerUps = GroupUtils.FindNodeDescendantsInGroup(initiator, "LifestealPowerUp");
            for (int i = 0; i < powerUps.Count; i++)
            {
                LifestealPowerUp lifestealPowerUp = (LifestealPowerUp)powerUps[i];
                lifestealPercentage += lifestealPowerUp.Percentage;
            }
        }

        velocity = Vector2.Zero;
        var bodies = explosionArea.GetOverlappingBodies();
        foreach (var body in bodies)
        {
            var method = body.GetType().GetMethod("Hit");
            inflictedDamage += (int)method?.Invoke(body, new object[] { damage });
        }
        initiator.Hit((int)(-inflictedDamage * lifestealPercentage));

        ((RocketProjectileGraphicsController)graphicsController).PlayExplosionAnimation(this);
        return inflictedDamage;
    }
}
