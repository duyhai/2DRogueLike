using Godot;

public partial class SimpleBullet : Bullet
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/weapons/projectiles/Bullet.tscn");

    public SimpleBullet() :
        base(new NullInputController(), new SimpleBulletPhysicsController(), new NullGraphicsController())
    {
        baseStats = new StatsInfo { MaxHealth = 0, MaxShield = 0, Damage = 0, Speed = 750 };
    }

    public override int HitTarget(Node collider)
    {
        int inflictedDamage = base.HitTarget(collider);
        QueueFree();
        return inflictedDamage;
    }
}