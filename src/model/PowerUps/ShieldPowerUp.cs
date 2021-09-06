using System;

public class ShieldPowerUp : PowerUp
{
    protected int absorptionAmount;

    public override void Initiate(GameObject target, GameObject initiator)
    {
        target.shield += absorptionAmount;
        base.Initiate(target, initiator);
    }

    public override StatsInfo UpdateStats(StatsInfo stats)
    {
        return new StatsInfo { MaxHealth = stats.MaxHealth, MaxShield = stats.MaxShield + absorptionAmount, Damage = stats.Damage, Speed = stats.Speed };
    }

    public int AbsorbDamage(int damage)
    {
        int remainingDamage = damage - absorptionAmount;
        if (remainingDamage < 0)
        {
            absorptionAmount = -remainingDamage;
        }
        else
        {
            QueueFree();
        }
        return remainingDamage;
    }
}