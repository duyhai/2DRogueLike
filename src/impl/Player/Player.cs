using Godot;
using System;

public class Player : KinematicBody2D
{
    public Vector2 velocity;
    public int speed = 100;
    public Vector2 lastOrientation;
    Timer bulletTimer;
    PackedScene bulletScene = (PackedScene)GD.Load("res://Bullet.tscn");
    PlayerInputController inputController = new PlayerInputController();
    PlayerPhysicsController physicsController = new PlayerPhysicsController();

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
    }

    public override void _Process(float delta)
    {
        inputController.Update(this);
    }

    public override void _PhysicsProcess(float delta)
    {
        physicsController.Update(this, delta);
    }

    public void Shoot(Vector2 vector)
    {
        if (bulletTimer.IsStopped())
        {
            bulletTimer.Start();
            var bullet = (Bullet)bulletScene.Instance();
            bullet.Initiate(lastOrientation.Angle(), Position + new Vector2(8, 8));
            GetParent().AddChild(bullet);
        }
    }
}
