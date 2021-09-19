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

    public override void UndoEffect()
    {
        GameObject parent = GetParent<GameObject>();
        parent.shield = Math.Min(parent.shield, parent.Stats.MaxShield - absorptionAmount);
        base.UndoEffect();
    }

    public override void Reset()
    {
        GameObject parent = GetParent<GameObject>();
        parent.shield = Math.Min(parent.shield, parent.Stats.MaxShield - absorptionAmount) + absorptionAmount;
        base.Reset();
    }
}