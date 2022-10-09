using Godot;
using Godot.Collections;
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
    public int shield;
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

        Connect("DeathSignal", this, nameof(PlayDeathAnimation));
    }

    public override void _Process(float delta)
    {
        if (!DisableInput)
        {
            inputController.Update(this);
        }
        graphicsController.Update(this);
    }

    public override void _PhysicsProcess(float delta)
    {
        physicsController.Update(this, delta);
    }

    public virtual int Hit(int damage)
    {
        if (isDead) { return 0; }

        int remainingDamage = damage;
        int inflictedDamage = 0;

        int newShield = Math.Min(Math.Max(shield - remainingDamage, 0), Stats.MaxShield);
        if (newShield < shield) // We don't want to add shield during lifesteal
        {
            inflictedDamage += shield - newShield;
            remainingDamage -= shield - newShield;
            shield = newShield;
        }

        int newHealth = Math.Min(Math.Max(health - remainingDamage, 0), Stats.MaxHealth);
        inflictedDamage += health - newHealth;
        health = newHealth;

        if (inflictedDamage != 0)
        {
            ((BasicGraphicsController)graphicsController).PlayHitAnimation(this);
        }

        if (health <= 0 && !isDead)
        {
            velocity = Vector2.Zero;
            health = 0;
            isDead = true;
            DisableInput = true;
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

    public virtual void OnAnimationFinished()
    {
        AnimatedSprite animSprite = GetNodeOrNull<AnimatedSprite>("AnimatedSprite");
        if (animSprite.Animation == "death")
        {
            ((BasicGraphicsController)graphicsController).PlayFadeAnimation(this);
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
                ((PowerUp)powerUps[i]).Reset();
                powerUp.QueueFree();
                return;
            }
        }
        AddChild(powerUp);
    }
}
