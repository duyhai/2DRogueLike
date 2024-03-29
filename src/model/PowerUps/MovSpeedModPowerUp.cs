using Godot;
using System;

public partial class MovSpeedModPowerUp : PowerUp
{
    protected float modifier;
    public float Modifier { get => modifier; }
    public override StatsInfo UpdateStats(StatsInfo stats)
    {
        return new StatsInfo { MaxHealth = stats.MaxHealth, MaxShield = stats.MaxShield, Damage = stats.Damage, Speed = (int)(stats.Speed * modifier) };
    }
}
