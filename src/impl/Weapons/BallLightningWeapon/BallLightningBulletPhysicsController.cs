using Godot;
using Godot.Collections;
using Utility;

public partial class BallLightningBulletPhysicsController : PhysicsController
{
    float radius = 50;

    public override void Update(GameObject gameObject, double delta)
    {
        BallLightningBullet ballLightningBullet = (BallLightningBullet)gameObject;

        var collision = ballLightningBullet.MoveAndCollide(ballLightningBullet.Velocity * (float)delta);
        if (collision != null)
        {
            ballLightningBullet.Velocity = Vector2.Zero;
            ballLightningBullet.QueueFree();
        }

        Timer bulletTimer = ballLightningBullet.GetNode<Timer>("BulletTimer");

        if (bulletTimer.IsStopped() && ballLightningBullet.TargetingState == BallLightningBullet.Targeting.Initial)
        {

            bulletTimer.Start();
            Array bodiesHit = new Array();
            ballLightningBullet.TargetingState = BallLightningBullet.Targeting.Chaining;
            Chaining(ballLightningBullet, bodiesHit);

            if (bodiesHit.Count != 0)
            {
                foreach (Node body in bodiesHit)
                {
                    ballLightningBullet.HitTarget(body);
                }
            }

            ballLightningBullet.BodiesHit = bodiesHit;
            ballLightningBullet.TargetingState = BallLightningBullet.Targeting.Animation;
        }

    }

    private void Chaining(GameObject entity, Array bodiesHit)
    {
        var bodies = entity.GetTree().GetNodesInGroup(NodeGroups.Enemy);
        foreach (Node2D body in bodies)
        {
            float distance = entity.GlobalPosition.DistanceTo(body.GlobalPosition);
            if (distance <= radius)
            {
                bodiesHit.Add(body);
            }
        }
    }
}