using Godot;

public class SimpleBullet : Bullet
{
    static int DAMAGE = 20;
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://Bullet.tscn");

    public SimpleBullet() :
        base(new NullInputController(), new SimpleBulletPhysicsController(), new NullGraphicsController(), DAMAGE)
    {
        speed = 750;
    }

    public override void HitTarget(KinematicCollision2D collision)
    {
        base.HitTarget(collision);
        QueueFree();
    }
}