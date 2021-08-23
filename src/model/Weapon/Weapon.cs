using System;
using Godot;

public abstract class Weapon : Node2D
{
    protected int bulletSpeed;
    public float weaponCooldown;
    protected int baseDamage;
    public int damage
    {
        get
        {
            float modifier = 1f;
            if (IsInsideTree())
            {
                var powerUps = GroupUtils.FindNodeDescendantsInGroup(GetParent<GameObject>(), "DamageModPowerUp");
                for (int i = 0; i < powerUps.Count; i++)
                {
                    DamageModPowerUp damageModPowerUp = (DamageModPowerUp)powerUps[i];
                    modifier += damageModPowerUp.Modifier;
                }
            }
            return (int)(baseDamage * modifier);
        }
    }
    protected Timer bulletTimer;
    protected PackedScene bulletScene;
    protected WeaponGraphicsController graphicsController;

    public Weapon(WeaponGraphicsController graphicsController, int damage)
    {
        this.graphicsController = graphicsController;
        baseDamage = damage;
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

        bullet.Initiate(GetParent<GameObject>(), vector.Angle(), tip != null ? tip.GlobalPosition : GlobalPosition, damage);
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