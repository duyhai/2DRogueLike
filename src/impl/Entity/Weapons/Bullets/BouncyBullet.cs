using Godot;

public class BouncyBullet : Bullet
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://BouncyBullet.tscn");

    public BouncyBullet() :
        base(new NullInputController(), new BouncyBulletPhysicsController(), new NullGraphicsController())
    {
        baseSpeed = 750;
    }

    public override int HitTarget(KinematicCollision2D collision)
    {
        int inflictedDamage = base.HitTarget(collision);
        var collider = (GameObject)collision.Collider;
        if (collider.GetType().BaseType.ToString() != nameof(Block))
        {
            QueueFree();
        }
        return inflictedDamage;
    }
}
