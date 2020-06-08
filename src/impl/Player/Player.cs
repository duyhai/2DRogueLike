using Godot;
using System;

public class Player : KinematicBody2D
{
    public Vector2 velocity;
    public int speed = 100;
    PlayerInputController inputController = new PlayerInputController();
    PlayerPhysicsController physicsController = new PlayerPhysicsController();


    public override void _Process(float delta)
    {
        inputController.Update(this);
    }

    public override void _PhysicsProcess(float delta)
    {
        physicsController.Update(this, delta);
    }
}
