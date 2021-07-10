using Godot;
using System;

public class PhantomBullet : Bullet
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/weapons/projectiles/PhantomBullet.tscn");

    public override void _Ready()
    {
        Area2D hitbox = GetNode<Area2D>("Area2D");
        hitbox.CollisionLayer = CollisionLayer;
        hitbox.CollisionMask = CollisionMask;
        GetNode<Timer>("Timer").Start();
    }

    public PhantomBullet() :
        base(new NullInputController(), new PhantomBulletPhysicsController(), new NullGraphicsController())
    {
        baseSpeed = 0;
    }

    public void OnTimerTimeout()
    {
        QueueFree();
    }
}
