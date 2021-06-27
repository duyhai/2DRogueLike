using Godot;

public class BurningPowerUp : PowerUp
{
    int damage = 1;

    public override void Effect()
    {
        var parent = GetParent();
        var method = parent.GetType().GetMethod("Hit");
        method?.Invoke(parent, new object[] { damage });
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