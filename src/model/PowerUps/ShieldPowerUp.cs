using System;

public partial class ShieldPowerUp : PowerUp
{
    protected int absorptionAmount;

    public override void Initiate(GameObject target, GameObject initiator)
    {
        target.Shield += absorptionAmount;
        base.Initiate(target, initiator);
    }

    public override StatsInfo UpdateStats(StatsInfo stats)
    {
        return new StatsInfo { MaxHealth = stats.MaxHealth, MaxShield = stats.MaxShield + absorptionAmount, Damage = stats.Damage, Speed = stats.Speed };
    }

    public override void UndoEffect()
    {
        GameObject parent = GetParent<GameObject>();
        parent.Shield = Math.Min(parent.Shield, parent.Stats.MaxShield - absorptionAmount);
        base.UndoEffect();
    }

    public override void Reset()
    {
        GameObject parent = GetParent<GameObject>();
        parent.Shield = Math.Min(parent.Shield, parent.Stats.MaxShield - absorptionAmount) + absorptionAmount;
        base.Reset();
    }
}