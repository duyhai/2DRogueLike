using Godot;
using System;

public class MovSpeedModPowerUp : PowerUp
{
    protected float modifier;
    public float Modifier { get => modifier; }
    public override void Effect() { }
    public override StatsInfo UpdateStats(StatsInfo stats)
    {
        return new StatsInfo(stats.Health, stats.Damage, (int)(stats.Speed * modifier), stats.LifeSteal);
    }
}
