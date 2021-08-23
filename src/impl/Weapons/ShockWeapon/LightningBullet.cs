using Godot;
using Godot.Collections;
using Utility;

public class LightningBullet : Bullet
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
    public bool ChainingFinished = false;

    public LightningBullet() :
        base(new NullInputController(), new LightningBulletPhysicsController(), new LightningBulletGraphicsController())
    {
        baseStats = new StatsInfo { MaxHealth = 0, Damage = 0, Speed = 0 };
    }

    public override void _Ready()
    {
        Area2D hitbox = GetNode<Area2D>("Area2D");
        hitbox.CollisionLayer = CollisionLayer;
        hitbox.CollisionMask = CollisionMask;
    }

    public void OnTimerTimeout()
    {
        QueueFree();
    }
}
