using Godot;
using System;

public abstract class GameObject : KinematicBody2D
{
    [Signal]
    public delegate void DeathSignal();
    public Vector2 velocity;
    protected int baseSpeed;
    public int speed
    {
        get
        {
            float modifier = 1f;
            if (IsInsideTree())
            {
                var powerUps = GroupUtils.FindNodeDescendantsInGroup(this, "MovSpeedModPowerUp");
                for (int i = 0; i < powerUps.Count; i++)
                {
                    MovSpeedModPowerUp movSpeedModPowerUp = (MovSpeedModPowerUp)powerUps[i];
                    modifier += movSpeedModPowerUp.Modifier;
                }
            }
            return (int)(baseSpeed * modifier);
        }
        set => baseSpeed = value;
    }
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

    public virtual int Hit(int damage)
    {
        if (isDead) { return 0; }

        int newHealth = Math.Min(Math.Max(health - damage, 0), maxHealth);
        int inflictedDamage = health - newHealth;
        health = newHealth;

        if (inflictedDamage != 0)
        {
            ((BasicGraphicsController)graphicsController).PlayHitAnimation(this);
        }

        if (health <= 0 && !isDead)
        {
            health = 0;
            isDead = true;
            disableInput = true;
            EmitSignal(nameof(DeathSignal));
        }

        FCTManager.Instance.ShowValue(Math.Abs(inflictedDamage).ToString(), GlobalPosition, inflictedDamage >= 0 ? Colors.Red : Colors.Green);
        return inflictedDamage;
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

    public void AddPowerUp(PowerUp powerUp)
    {
        var powerUps = GroupUtils.FindNodeDescendantsInGroup(this, "PowerUp");
        for (int i = 0; i < powerUps.Count; i++)
        {
            if (powerUps[i].GetType() == powerUp.GetType())
            {
                ((PowerUp)powerUps[i]).UndoEffect();
            }
        }
        AddChild(powerUp);
    }
}
