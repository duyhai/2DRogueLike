using Godot;
using System;

public class LifestealPowerUp : PowerUp
{
    protected float percentage = 0.5f;
    public float Percentage { get => percentage; }
    public override void DamageEffect(GameObject initiator, Node target, int damage)
    {
        DamageUtil.HandleDamage(initiator, initiator, (int)(damage * -percentage));
    }
}