using Godot;
using System;

public class DamageModPowerUp : PowerUp
{
    protected float modifier;
    public float Modifier { get => modifier; }
    public override StatsInfo UpdateStats(StatsInfo stats)
    {
        return new StatsInfo(stats.MaxHealth, (int)(stats.Damage * modifier), stats.Speed);
    }
}
