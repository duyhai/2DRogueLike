using System;
using Godot;

public abstract class Weapon : Node2D
{
    protected float damageMultiplier;
    protected Timer bulletTimer;
    protected PackedScene bulletScene;
    protected WeaponGraphicsController graphicsController;

    public Weapon(WeaponGraphicsController graphicsController, float damageMultiplier)
    {
        this.graphicsController = graphicsController;
        this.damageMultiplier = damageMultiplier;
    }

    public override void _Process(float delta)
    {
        graphicsController?.Update(this);
    }

    public virtual bool Shoot(Vector2 vector, uint collisionLayer, uint collisionMask)
    {
        if (!bulletTimer.IsStopped())
        {
            return false;
        }

        bulletTimer.Start();
        var bullet = (Bullet)bulletScene.Instance();

        var tip = GetNodeOrNull<Node2D>("Sprite/Tip");

        GameObject initiator = GetParent<GameObject>();

        bullet.Initiate(initiator, vector.Angle(), tip != null ? tip.GlobalPosition : GlobalPosition, (int)(initiator.Stats.Damage * damageMultiplier));
        bullet.CollisionLayer = collisionLayer;
        bullet.CollisionMask = collisionMask;

        var world = GetParent().GetParent<Node>();
        if (world != null)
        {
            world.AddChild(bullet);
        }

        return true;
    }

    public void SetWeaponCooldown(float weaponCooldown)
    {
        bulletTimer.WaitTime = weaponCooldown;
    }
}