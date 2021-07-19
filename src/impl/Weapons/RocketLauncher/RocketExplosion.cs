using Godot;
using System;

public class RocketExplosion : GameObject
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/weapons/projectiles/RocketExplosion.tscn");
    public int damage;
    public GameObject initiator;

    public RocketExplosion() :
        base(new NullInputController(), new RocketExplosionPhysicsController(), new RocketExplosionGraphicsController())
    {
        baseSpeed = 0;
    }

    public override void _Ready()
    {
        Area2D hitbox = GetNode<Area2D>("Area2D");
        hitbox.CollisionLayer = CollisionLayer;
        hitbox.CollisionMask = CollisionMask;
        ((RocketExplosionGraphicsController)graphicsController).PlayExplosionAnimation(this);
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

    public void OnAnimatedSpriteAnimationFinished()
    {
        QueueFree();
    }
}
