using Godot;

public partial class TimedCameraEffect : ICameraEffect
{
    public float Timer
    {
        get;
        protected set;
    }
    public float Duration
    {
        get;
        protected set;
    }
    public bool IsEnded
    {
        get
        {
            return Timer <= 0f;
        }
    }

    protected TimedCameraEffect(float duration)
    {
        this.Duration = duration;
    }

    protected TimedCameraEffect()
    { }

    public virtual void Update(Camera2D camera, double delta)
    {
        if (Timer == 0f)
        {
            return;
        }

        Timer -= (float)delta;
        if (Timer < 0f)
        {
            Timer = 0f;
            TimerEnded();
        }
    }

    protected virtual void TimerEnded() { }

    public virtual void Start(Camera2D camera)
    {
        Timer = Duration;
    }
}