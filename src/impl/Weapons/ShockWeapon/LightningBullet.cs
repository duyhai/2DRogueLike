using Godot;
using System;

public class LightningBullet : GameObject
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/weapons/projectiles/LightningBullet.tscn");
    public int damage;
    public GameObject initiator;


    public LightningBullet() :
        base(new NullInputController(), new LightningBulletPhysicsController(), new NullGraphicsController())
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

    public void Initiate(GameObject initiator, Vector2 position, int damage)
    {
        Position = position;
        this.damage = damage;
        this.initiator = initiator;
    }
}
