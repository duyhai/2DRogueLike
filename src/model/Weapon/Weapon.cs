using System;
using Godot;

public abstract class Weapon : Node2D
{
    protected float damageMultiplier;
    protected Timer bulletTimer;
    protected PackedScene bulletScene;
    protected WeaponGraphicsController graphicsController;
    protected PackedScene weaponIconScene = null;

    public Weapon(WeaponGraphicsController graphicsController, float damageMultiplier)
    {
        this.graphicsController = graphicsController;
        this.damageMultiplier = damageMultiplier;
    }

    public override void _Process(float delta)
    {
        graphicsController?.Update(this);
    }

    public virtual bool Shoot(Vector2 vector)
    {
        if (!bulletTimer.IsStopped())
        {
            return false;
        }

        bulletTimer.Start();
        var bullet = (Bullet)bulletScene.Instance();

        var weaponTip = GetNodeOrNull<Node2D>("Sprite/Tip");

        GameObject initiator = GetParent<GameObject>();

        Vector2 weaponTipPosition = weaponTip != null ? weaponTip.GlobalPosition : GlobalPosition;
        int damage = (int)(initiator.Stats.Damage * damageMultiplier);
        bullet.Initiate(initiator, vector.Angle(), weaponTipPosition, damage);
        bullet.CollisionLayer = initiator.CollisionLayer;
        bullet.CollisionMask = initiator.CollisionMask - CollisionLayers.Water;

        var world = GetParent().GetParent<Node>();

        world?.AddChild(bullet);

        return true;
    }

    public void SetWeaponCooldown(float weaponCooldown)
    {
        bulletTimer.WaitTime = weaponCooldown;
    }

    public TextureRect GetWeaponIcon()
    {
        if (weaponIconScene == null) return null;

        return (TextureRect)weaponIconScene.Instance();
    }
}