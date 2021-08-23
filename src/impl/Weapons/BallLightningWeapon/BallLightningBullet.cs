using Godot;
using Godot.Collections;

public class BallLightningBullet : Bullet
{
    public enum Targeting
    {
        Initial,
        Chaining,
        Animation
    }
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/weapons/projectiles/BallLightningBullet.tscn");
    public Targeting TargetingState = Targeting.Initial;
    public Array BodiesHit = new Array();

    public BallLightningBullet() :
        base(new NullInputController(), new BallLightningBulletPhysicsController(), new BallLightningBulletGraphicsController())
    {
        baseSpeed = 70;
    }
}