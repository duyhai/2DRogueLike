using Godot;
using System;

public class MovSpeedModPowerUp : PowerUp
{
    protected float modifier;
    public float Modifier { get => modifier; }
    public override StatsInfo UpdateStats(StatsInfo stats)
    {
        return new StatsInfo(stats.MaxHealth, stats.Damage, (int)(stats.Speed * modifier));
    }
}
