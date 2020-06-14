using Godot;
using System;

public class Bullet : KinematicBody2D
{
    public Vector2 velocity;
    int speed = 500;
    BulletPhysicsController bulletPhysicsController = new BulletPhysicsController();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void Initiate(float rotation, Vector2 position)
    {
        Position = position;
        velocity = new Vector2(speed, 0).Rotated(rotation);
    }

    public override void _PhysicsProcess(float delta)
    {
        bulletPhysicsController.Update(this, delta);
    }

    public void OnVisibilityNotifier2DScreenExited()
    {
        QueueFree();
    }
}
