using Godot;
using System;

public class MovSpeedModPowerUp : PowerUp
{
    protected float modifier;
    public float Modifier { get => modifier; }
    public override StatsInfo UpdateStats(StatsInfo stats)
    {
        return new StatsInfo { MaxHealth = stats.MaxHealth, Damage = stats.Damage, Speed = (int)(stats.Speed * modifier) };
    }
}
