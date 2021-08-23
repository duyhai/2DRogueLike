using Godot;
using System;

public class MeleeBullet : Bullet
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/weapons/projectiles/MeleeBullet.tscn");

    public override void _Ready()
    {
        Area2D swingingArea = GetNode<Area2D>("Area2D");
        swingingArea.CollisionLayer = CollisionLayer;
        swingingArea.CollisionMask = CollisionMask;
        GetNode<Timer>("Timer").Start();
    }

    public MeleeBullet() :
        base(new NullInputController(), new MeleeBulletPhysicsController(), new NullGraphicsController())
    {
        baseStats = new StatsInfo { MaxHealth = 0, Damage = 0, Speed = 0 };
    }

    public void OnTimerTimeout()
    {
        QueueFree();
    }
}
