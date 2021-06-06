using Godot;

public abstract class PowerUp : Node
{
    public abstract void Effect();

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