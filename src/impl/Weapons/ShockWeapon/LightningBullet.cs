using Godot;
using System;

public class LightningBullet : Bullet
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/weapons/projectiles/LIghtningBullet.tscn");

    public override void _Ready()
    {
        Area2D hitbox = GetNode<Area2D>("Area2D");
        hitbox.CollisionLayer = CollisionLayer;
        hitbox.CollisionMask = CollisionMask;
        GetNode<Timer>("Timer").Start();
    }

    public LightningBullet() :
        base(new NullInputController(), new LightningBulletPhysicsController(), new NullGraphicsController())
    {
        baseSpeed = 0;
    }

    public void OnTimerTimeout()
    {
        QueueFree();
    }
}
