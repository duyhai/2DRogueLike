using Godot;
using System;

public abstract class GameObject : KinematicBody2D
{
    [Signal]
    public delegate void DeathSignal();
    public Vector2 velocity;
    public int speed;
    public int health;
    public int maxHealth;
    public bool isDead = false;
    public bool disableInput = false;
    protected InputController inputController;
    protected PhysicsController physicsController;
    protected GraphicsController graphicsController;

    public GameObject(InputController inputController, PhysicsController physicsController, GraphicsController graphicsController)
    {
        this.inputController = inputController;
        this.physicsController = physicsController;
        this.graphicsController = graphicsController;

        Connect("DeathSignal", this, nameof(PlayDeathAnimation));
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

        if (health > 0)
        {
            ((BasicGraphicsController)graphicsController).PlayHitAnimation(this);
            FCTManager.Instance.ShowValue(damage.ToString(), GlobalPosition);
        }

        if (health <= 0 && !isDead)
        {
            health = 0;
            isDead = true;
            disableInput = true;
            EmitSignal(nameof(DeathSignal));
        }
    }

    public void ResetHealth()
    {
        health = maxHealth;
        isDead = false;
    }

    public virtual void OnAnimationFinished(string animationName)
    {
        if (animationName == "death")
        {
            Death();
        }
    }

    public virtual void PlayDeathAnimation()
    {
        ((BasicGraphicsController)graphicsController).PlayDeathAnimation(this);
    }

    public virtual void Death()
    {
        QueueFree();
    }
}
