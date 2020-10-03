using Godot;
using System;

public abstract class GameObject : KinematicBody2D
{
    public Vector2 velocity;
    public int speed;
    public int health;
    public int maxHealth;
    protected InputController inputController;
    protected PhysicsController physicsController;
    protected GraphicsController graphicsController;

    public GameObject(InputController inputController, PhysicsController physicsController, GraphicsController graphicsController){
        this.inputController = inputController;
        this.physicsController = physicsController;
        this.graphicsController = graphicsController;
    }

    public override void _Process(float delta)
    {
        inputController.Update(this);
        graphicsController.Update(this);
    }

    public override void _PhysicsProcess(float delta)
    {
        physicsController.Update(this, delta);
    }

    public virtual void Hit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            QueueFree();
        }
    }
}
