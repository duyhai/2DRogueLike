using Godot;
using System;

public abstract class GameObject : KinematicBody2D
{
    [Signal]
    public delegate void DeathSignal();
    public Vector2 velocity;
    protected StatsInfo baseStats;
    public StatsInfo Stats
    {
        get
        {
            StatsInfo stats = baseStats;
            if (IsInsideTree())
            {
                var powerUps = GroupUtils.FindNodeDescendantsInGroup(this, "PowerUp");
                for (int i = 0; i < powerUps.Count; i++)
                {
                    PowerUp powerUp = (PowerUp)powerUps[i];
                    stats = powerUp.UpdateStats(stats);
                }
            }
            return stats;
        }
        set { baseStats = value; }
    }
    protected int baseSpeed;
    public int health;
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

        int newHealth = Math.Min(Math.Max(health - damage, 0), Stats.MaxHealth);
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
        health = Stats.MaxHealth;
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
