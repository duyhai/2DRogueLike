using Godot;

public abstract class PowerUp : Node2D
{
    public abstract void Effect();
    public virtual StatsInfo UpdateStats(StatsInfo stats) { return null; }

    public override void _Ready()
    {
        StartTimers();
    }

    public virtual void UndoEffect()
    {
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
}