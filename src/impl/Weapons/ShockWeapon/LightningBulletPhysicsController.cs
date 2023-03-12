using Godot;
using Godot.Collections;
using Utility;

public class LightningBulletPhysicsController : PhysicsController
{
    float radius = 75;
    float fov = Mathf.Pi / 4;

    public override void Update(GameObject gameObject, float delta)
    {
        Timer bulletTimer = gameObject.GetNode<Timer>("BulletTimer");
        if (bulletTimer.IsStopped())
        {
            LightningBullet lightningBullet = (LightningBullet)gameObject;

            Array bodiesHit = new Array();
            lightningBullet.TargetingState = LightningBullet.Targeting.Chaining;
            Chaining(gameObject.GetNode<Player>("../Player"), bodiesHit);

            if (bodiesHit.Count != 0)
            {
                foreach (Node body in bodiesHit)
                {
                    lightningBullet.HitTarget(body);
                }
            }

            lightningBullet.SetPhysicsProcess(lightningBullet.DamageMultipleTimes);

            if (bodiesHit.Count != 0)
            {
                bulletTimer.Start();
            }

            lightningBullet.BodiesHit = bodiesHit;
            lightningBullet.TargetingState = LightningBullet.Targeting.Animation;
        }
    }

    private void Chaining(GameObject entity, Array bodiesHit, int maxBodies = 3)
    {
        if (maxBodies == 0)
        {
            return;
        }

        var bodies = entity.GetTree().GetNodesInGroup(NodeGroups.Enemy);
        Vector2 lastBodyGlobalPosition;
        if (bodiesHit.Count == 0)
        {
            lastBodyGlobalPosition = entity.GlobalPosition;
        }
        else
        {
            lastBodyGlobalPosition = ((GameObject)bodiesHit[bodiesHit.Count - 1]).GlobalPosition;
        }

        GameObject nearestBody = (GameObject)ArrayUtil.Min(ref bodies, (object x, object y) =>
        {
            if (bodiesHit.Contains((GameObject)y))
            {
                return x;
            }
            if (bodiesHit.Count == 0 && entity.velocity.Length() == 0f && !inFieldOfView(entity, (GameObject)y))
            {
                return x;
            }
            float dy = ((GameObject)y).GlobalPosition.DistanceTo(lastBodyGlobalPosition);
            if (dy > radius)
            {
                return x;
            }
            if (x == null)
            {
                return y;
            }
            float dx = ((GameObject)x).GlobalPosition.DistanceTo(lastBodyGlobalPosition);

            return dx < dy ? x : y;
        });

        if (nearestBody != null)
        {
            bodiesHit.Add(nearestBody);
        }

        Chaining(entity, bodiesHit, maxBodies - 1);
    }
    bool inFieldOfView(GameObject entity, GameObject body)
    {
        Player player = (Player)entity;
        Vector2 relativeMouseLoc = player.ViewDirection.Normalized();
        Vector2 relativeNearestBodyLoc = (body.GlobalPosition - entity.GlobalPosition).Normalized();
        return Mathf.Cos(fov) < relativeMouseLoc.Dot(relativeNearestBodyLoc);
    }
}