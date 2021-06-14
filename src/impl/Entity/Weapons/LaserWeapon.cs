using Godot;
using System;

public class LaserWeapon : Weapon
{
    bool isCasting = false;
    RayCast2D rayCast2D;
    SoundPlayer soundPlayer;

    public LaserWeapon() : base(new LaserWeaponGraphicsController(), 3) { }

    public override bool Shoot(Vector2 vector, uint collisionLayer, uint collisionMask)
    {
        GetNode<RayCast2D>("Tip/RayCast2D").CollisionMask = collisionMask;
        setIsCasting(true);
        bulletTimer.Start();
        return true;
    }

    public override void _Ready()
    {
        // This BulletTimer is repurposed to help with 
        // disabling the laser when the attack button is released.
        // We can to this, because the laser doesn't have a cooldown.
        bulletTimer = GetNode<Timer>("BulletTimer");
        SetPhysicsProcess(false);
        GetNode<Line2D>("Tip/RayCast2D/Line2D").Points[1] = Vector2.Zero;
        rayCast2D = GetNode<RayCast2D>("Tip/RayCast2D");
    }

    public override void _PhysicsProcess(float delta)
    {
        Vector2 castPoint = rayCast2D.CastTo;
        rayCast2D.ForceRaycastUpdate();
        var collisionParticles2D = GetNode<Particles2D>("Tip/RayCast2D/CollisionParticles2D");
        collisionParticles2D.Emitting = rayCast2D.IsColliding();

        if (rayCast2D.IsColliding())
        {
            castPoint = rayCast2D.ToLocal(rayCast2D.GetCollisionPoint());
            var body = rayCast2D.GetCollider();
            var method = body.GetType().GetMethod("Hit");
            method?.Invoke(body, new object[] { damage });
            collisionParticles2D.GlobalRotation = rayCast2D.GetCollisionNormal().Angle();
            collisionParticles2D.Position = castPoint;
        }
        GetNode<Line2D>("Tip/RayCast2D/Line2D").SetPointPosition(1, castPoint);
    }

    void setIsCasting(bool cast)
    {
        if (cast && !isCasting)
        {
            ((LaserWeaponGraphicsController)graphicsController).appear(this);
            soundPlayer = SoundManager.Instance.PlaySound(SoundPaths.LaserBeam, true);
        }
        else if (!cast && isCasting)
        {
            GetNode<Particles2D>("Tip/RayCast2D/CollisionParticles2D").Emitting = false;
            ((LaserWeaponGraphicsController)graphicsController).disappear(this);
            soundPlayer.QueueFree();
        }

        isCasting = cast;
        SetPhysicsProcess(isCasting);
        GetNode<Particles2D>("Tip/RayCast2D/CastingParticles2D").Emitting = cast;
    }

    public void OnBulletTimerTimeout()
    {
        setIsCasting(false);
    }
}
