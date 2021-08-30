using Godot;

public abstract class PowerUp : Node2D
{
    protected GameObject initiator;

    public void Initiate(GameObject initiator)
    {
        this.initiator = initiator;
    }

    public virtual void Effect() { }

    public virtual void DamageEffect(GameObject initiator, Node target, int damage) { }

    public virtual StatsInfo UpdateStats(StatsInfo stats) { return stats; }

    public override void _Ready()
    {
        StartTimers();
    }

    public virtual void UndoEffect()
    {
        GetNode<Timer>("EffectTimer").Stop();
        QueueFree();
    }

    public virtual void StartTimers()
    {
        GetNode<Timer>("DurationTimer").Start();
    }

    public virtual void OnDurationTimerTimeout()
    {
        UndoEffect();
    }

    public virtual void Reset()
    {
        StartTimers();
    }

    public virtual void OnEffectTimerTimeout()
    {
        Effect();
    }
}