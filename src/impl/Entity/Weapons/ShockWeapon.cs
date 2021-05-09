using Godot;
using Godot.Collections;
using System.Linq;
using Utility;

public class ShockWeapon : Weapon
{
    int damage = 1000;
    float radius = 75;
    float fov = Mathf.Cos(Mathf.Pi / 4);

    public ShockWeapon() : base(new ShockWeaponGraphicsController()) { }

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
    }

    public override bool Shoot(Vector2 vector, uint collisionLayer, uint collisionMask)
    {
        if (!bulletTimer.IsStopped())
        {
            return false;
        }
        Array bodiesHit = new Array();
        Chaining((GameObject)GetParent(), bodiesHit);
        if (bodiesHit.Count != 0)
        {
            bulletTimer.Start();
        }
        else
        {
            return false;
        }

        ((ShockWeaponGraphicsController)graphicsController).ChainingBodiesAnimation(this, bodiesHit);

        foreach (var body in bodiesHit)
        {
            var method = body.GetType().GetMethod("Hit");
            method?.Invoke(body, new object[] { damage });
        }
        SoundManager.Instance.PlaySound(SoundPaths.Lightning);
        return true;
    }

    private void Chaining(GameObject entity, Array bodiesHit, int maxBodies = 3)
    {
        if (maxBodies == 0)
        {
            return;
        }

        var bodies = GetTree().GetNodesInGroup("enemy");
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
            if (bodiesHit.Count == 0 && !inFieldOfView(entity, (GameObject)y))
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
        Vector2 relativeMouseLoc = (entity.GetGlobalMousePosition() - entity.GlobalPosition).Normalized();
        Vector2 relativeNearestBodyLoc = (body.GlobalPosition - entity.GlobalPosition).Normalized();
        return fov < relativeMouseLoc.Dot(relativeNearestBodyLoc);
    }
}
