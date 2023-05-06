using Godot;
using System;

public abstract partial class GameObject : CharacterBody2D
{
    [Signal]
    public delegate void DeathSignalEventHandler();
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
    protected int shield;
    public int health;
    public virtual int Shield
    {
        get { return shield; }
        set { shield = value; }
    }
    public bool isDead = false;
    public bool DisableInput = false;
    public bool Sliding = false;
    protected InputController inputController;
    protected PhysicsController physicsController;
    protected GraphicsController graphicsController;

    public GameObject(InputController inputController, PhysicsController physicsController, GraphicsController graphicsController)
    {
        this.inputController = inputController;
        this.physicsController = physicsController;
        this.graphicsController = graphicsController;

        Connect("DeathSignal", new Callable(this, nameof(OnDeathStart)));
    }

    public override void _Process(double delta)
    {
        if (!DisableInput)
        {
            inputController.Update(this);
        }
        graphicsController.Update(this);
    }

    public override void _PhysicsProcess(double delta)
    {
        physicsController.Update(this, delta);
    }

    public virtual int Hit(int damage)
    {
        if (isDead) { return 0; }

        int remainingDamage = damage;
        int inflictedDamage = 0;

        int newShield = Math.Min(Math.Max(Shield - remainingDamage, 0), Stats.MaxShield);
        if (newShield < Shield) // We don't want to add shield during lifesteal
        {
            inflictedDamage += Shield - newShield;
            remainingDamage -= Shield - newShield;
            Shield = newShield;
        }

        int newHealth = Math.Min(Math.Max(health - remainingDamage, 0), Stats.MaxHealth);
        inflictedDamage += health - newHealth;
        health = newHealth;

        if (inflictedDamage > 0)
        {
            ((BasicGraphicsController)graphicsController).PlayHitAnimation(this);
        }

        if (health <= 0 && !isDead)
        {
            Velocity = Vector2.Zero;
            health = 0;
            isDead = true;
            DisableInput = true;
            EmitSignal("DeathSignal");
        }

        if (inflictedDamage != 0)
        {
            FCTManager.Instance.ShowValue(Math.Abs(inflictedDamage).ToString(), GlobalPosition, inflictedDamage >= 0 ? Colors.Red : Colors.Green);
        }
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
            OnDeathFinished();
        }
    }

    public virtual void OnAnimationFinished()
    {
        AnimatedSprite2D animSprite = GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D");
        if (animSprite.Animation == "death")
        {
            ((BasicGraphicsController)graphicsController).PlayFadeAnimation(this);
        }
    }

    public virtual void OnDeathStart()
    {
        ((BasicGraphicsController)graphicsController).PlayDeathAnimation(this);
    }

    public virtual void OnDeathFinished()
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
                ((PowerUp)powerUps[i]).Reset();
                powerUp.QueueFree();
                return;
            }
        }
        AddChild(powerUp);
    }
}
