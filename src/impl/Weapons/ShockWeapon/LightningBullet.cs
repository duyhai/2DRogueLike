using Godot;
using Godot.Collections;
using Utility;

public partial class LightningBullet : Bullet
{
    public enum Targeting
    {
        Initial,
        Chaining,
        Animation
    }

    public Targeting TargetingState = Targeting.Initial;

    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/weapons/projectiles/LightningBullet.tscn");
    public Array BodiesHit = new Array();
    public bool DamageMultipleTimes = false;

    public LightningBullet() :
        base(new NullInputController(), new LightningBulletPhysicsController(), new LightningBulletGraphicsController())
    {
        baseStats = new StatsInfo { MaxHealth = 0, MaxShield = 0, Damage = 0, Speed = 0 };
    }

    public LightningBullet(InputController inputController, PhysicsController physicsController, GraphicsController graphicsController) :
        base(inputController, physicsController, graphicsController)
    { }

    public void OnTimerTimeout()
    {
        QueueFree();
    }
}
