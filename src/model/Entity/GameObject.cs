using Godot;
using System;

public abstract class GameObject : KinematicBody2D
{
<<<<<<< HEAD
=======
    [Signal]
    public delegate void DeathSignal();
>>>>>>> master
    public Vector2 velocity;
    public int speed;
    public int health;
    public int maxHealth;
<<<<<<< HEAD
=======
    public bool isDead = false;
>>>>>>> master
    protected InputController inputController;
    protected PhysicsController physicsController;
    protected GraphicsController graphicsController;

<<<<<<< HEAD
    public GameObject(InputController inputController, PhysicsController physicsController, GraphicsController graphicsController){
=======
    public GameObject(InputController inputController, PhysicsController physicsController, GraphicsController graphicsController)
    {
>>>>>>> master
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

<<<<<<< HEAD
    public override void _Process(float delta)
    {
        inputController.Update(this);
        graphicsController.Update(this);
    }

=======
>>>>>>> master
    public override void _PhysicsProcess(float delta)
    {
        physicsController.Update(this, delta);
    }

    public virtual void Hit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
<<<<<<< HEAD
            QueueFree();
        }
    }
=======
            isDead = true;
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
            QueueFree();
        }
    }

    public void PlayDeathAnimation()
    {
        ((BasicGraphicsController)graphicsController).PlayDeathAnimation(this);
    }
>>>>>>> master
}
