using Godot;

public class BouncyBullet : Bullet
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/weapons/projectiles/BouncyBullet.tscn");

    public BouncyBullet() :
        base(new NullInputController(), new BouncyBulletPhysicsController(), new NullGraphicsController())
    {
        baseStats = new StatsInfo { MaxHealth = 0, MaxShield = 0, Damage = 0, Speed = 750 };
    }

    public override int HitTarget(Node collider)
    {
        int inflictedDamage = base.HitTarget(collider);
        if (collider.GetType().BaseType.ToString() != nameof(Block))
        {
            QueueFree();
        }
        return inflictedDamage;
    }
}
