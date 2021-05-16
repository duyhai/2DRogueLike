using Godot;
using System;

public class LaserWeapon : Weapon
{
    bool isCasting = false;
    int damage = 3;
    RayCast2D rayCast2D;

    public LaserWeapon() : base(new LaserWeaponGraphicsController()) { }

    public override bool Shoot(Vector2 vector, uint collisionLayer, uint collisionMask)
    {
        GetNode<RayCast2D>("Tip/RayCast2D").CollisionMask = collisionMask;
        setIsCasting(true);
        bulletTimer.Start();
        return true;
    }

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        SetPhysicsProcess(false);
        GetNode<Line2D>("Tip/RayCast2D/Line2D").Points[1] = Vector2.Zero;
        rayCast2D = GetNode<RayCast2D>("Tip/RayCast2D");
    }

    public override void _PhysicsProcess(float delta)
    {
        Vector2 castPoint = rayCast2D.CastTo;
        rayCast2D.ForceRaycastUpdate();

        if (rayCast2D.IsColliding())
        {
            castPoint = rayCast2D.ToLocal(rayCast2D.GetCollisionPoint());
            var body = rayCast2D.GetCollider();
            var method = body.GetType().GetMethod("Hit");
            method?.Invoke(body, new object[] { damage });
        }
        GetNode<Line2D>("Tip/RayCast2D/Line2D").SetPointPosition(1, castPoint);
    }

    void setIsCasting(bool cast)
    {
        if (cast && !isCasting)
        {
            ((LaserWeaponGraphicsController)graphicsController).appear(this);
        }
        else if (!cast && isCasting)
        {
            ((LaserWeaponGraphicsController)graphicsController).disappear(this);
        }

        isCasting = cast;
        SetPhysicsProcess(isCasting);
    }

    public void OnBulletTimerTimeout()
    {
        setIsCasting(false);
    }
}
