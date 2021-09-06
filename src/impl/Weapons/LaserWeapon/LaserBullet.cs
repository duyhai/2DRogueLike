using Godot;
using System;

public class LaserBullet : Bullet
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/weapons/projectiles/LaserBullet.tscn");
    bool isCasting = false;
    SoundPlayer soundPlayer;

    public LaserBullet() :
        base(new NullInputController(), new LaserBulletPhysicsController(), new LaserBulletGraphicsController())
    {
        baseStats = new StatsInfo { MaxHealth = 0, MaxShield = 0, Damage = 0, Speed = 0 };
    }

    public override void _Ready()
    {
        SetPhysicsProcess(false);
    }

    public override void Initiate(GameObject initiator, float rotation, Vector2 position, int damage)
    {
        CollisionLayer = initiator.CollisionLayer;
        CollisionMask = initiator.CollisionMask;
        RayCast2D rayCast2D = GetNode<RayCast2D>("RayCast2D");
        rayCast2D.CollisionMask = CollisionMask;
        base.Initiate(initiator, rotation, position, damage);
    }

    public void SetIsCasting(bool cast)
    {
        if (cast && !isCasting)
        {
            ((LaserBulletGraphicsController)graphicsController).Appear(this);
            soundPlayer = SoundManager.Instance.PlaySound(SoundPaths.LaserBeam, true);
        }
        else if (!cast && isCasting)
        {
            GetNode<Particles2D>("RayCast2D/CollisionParticles2D").Emitting = false;
            ((LaserBulletGraphicsController)graphicsController).Disappear(this);
            soundPlayer.QueueFree();
        }

        isCasting = cast;
        SetPhysicsProcess(isCasting);
        GetNode<Particles2D>("RayCast2D/CastingParticles2D").Emitting = cast;
    }
}
