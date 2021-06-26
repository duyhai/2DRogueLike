using Godot;

public class SimpleBullet : Bullet
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/weapons/projectiles/Bullet.tscn");

    public SimpleBullet() :
        base(new NullInputController(), new SimpleBulletPhysicsController(), new NullGraphicsController())
    {
        baseSpeed = 750;
    }

    public override int HitTarget(KinematicCollision2D collision)
    {
        int inflictedDamage = base.HitTarget(collision);
        QueueFree();
        return inflictedDamage;
    }
}