using Godot;

public class LaserBulletPhysicsController : PhysicsController
{
    public override void Update(GameObject gameObject, float delta)
    {
        LaserBullet laserBullet = (LaserBullet)gameObject;
        RayCast2D rayCast2D = gameObject.GetNode<RayCast2D>("RayCast2D");
        Vector2 castPoint = rayCast2D.CastTo;
        rayCast2D.ForceRaycastUpdate();
        var collisionParticles2D = gameObject.GetNode<Particles2D>("RayCast2D/CollisionParticles2D");
        collisionParticles2D.Emitting = rayCast2D.IsColliding();

        if (rayCast2D.IsColliding())
        {
            castPoint = rayCast2D.ToLocal(rayCast2D.GetCollisionPoint());
            laserBullet.HitTarget((Node)rayCast2D.GetCollider());

            collisionParticles2D.GlobalRotation = rayCast2D.GetCollisionNormal().Angle();
            collisionParticles2D.Position = castPoint;
        }
        gameObject.GetNode<Line2D>("RayCast2D/Line2D").SetPointPosition(1, castPoint);
    }
}