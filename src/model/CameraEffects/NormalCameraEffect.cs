using Godot;

public class NormalCameraEffect : ICameraEffect
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

    protected NormalCameraEffect(float duration)
    {
        this.Duration = duration;
    }

    protected NormalCameraEffect()
    { }

    public virtual void Update(Camera2D camera, float delta)
    {
        if (Timer == 0f)
        {
            return;
        }

        Timer -= delta;
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