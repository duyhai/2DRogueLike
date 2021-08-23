using Godot;

public class BurningPowerUp : PowerUp
{
    float damageMultiplier = 0.1f;

    public override void Effect()
    {
        var parent = GetParent<GameObject>();
        DamageUtil.HandleDamage(initiator, parent, (int)(initiator.Stats.Damage * damageMultiplier));
    }

    public override void StartTimers()
    {
        base.StartTimers();
        GetNode<Timer>("EffectTimer").Start();
    }

    public void OnEffectTimerTimeout()
    {
        Effect();
    }
}