using Godot;
using System;

public class RocketHitBullet : Bullet
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/weapons/projectiles/RocketHitBullet.tscn");

    public RocketHitBullet() :
        base(new NullInputController(), new RocketHitBulletPhysicsController(), new RocketHitBulletGraphicsController())
    {
        baseSpeed = 0;
    }

    public override void _Ready()
    {
        Area2D hitbox = GetNode<Area2D>("Area2D");
        hitbox.CollisionLayer = CollisionLayer;
        hitbox.CollisionMask = CollisionMask;
        ((RocketHitBulletGraphicsController)graphicsController).PlayExplosionAnimation(this);
    }

    public void OnTimerTimeout()
    {
        QueueFree();
    }

    public override void Initiate(GameObject initiator, float rotation, Vector2 position, int damage)
    {
        base.Initiate(initiator, rotation, position, damage);
    }

    public void OnAnimatedSpriteAnimationFinished() 
    {
        QueueFree();
    }
}
