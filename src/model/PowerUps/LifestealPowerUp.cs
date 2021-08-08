using Godot;
using System;

public class LifestealPowerUp : PowerUp
{
    protected float percentage = 0.5f;
    public float Percentage { get => percentage; }
    public override void Effect() { }
    public override StatsInfo UpdateStats(StatsInfo stats)
    {
        return new StatsInfo(stats.Health, stats.Speed, stats.Damage, (int)(stats.LifeSteal + percentage));
    }
}
