using Godot;
using System;

public partial class DamageModPowerUp : PowerUp
{
    protected float modifier;
    public float Modifier { get => modifier; }
    public override StatsInfo UpdateStats(StatsInfo stats)
    {
        return new StatsInfo { MaxHealth = stats.MaxHealth, MaxShield = stats.MaxShield, Damage = (int)(stats.Damage * modifier), Speed = stats.Speed };
    }
}
