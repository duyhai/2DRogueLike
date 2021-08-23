using Godot;
using Godot.Collections;

public class BallLightningBulletV2 : LightningBullet
{
    public static new PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/weapons/projectiles/BallLightningBulletV2.tscn");

    public BallLightningBulletV2() :
        base(new NullInputController(), new BallLightningBulletV2PhysicsController(), new LightningBulletGraphicsController())
    {
        baseSpeed = 70;
        DamageMultipleTimes = true;
    }
}