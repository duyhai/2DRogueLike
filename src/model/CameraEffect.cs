using Godot;

public abstract class CameraEffect
{
    float timer = 0;
    float duration = 0;

    public CameraEffect(float duration)
    {
        this.duration = duration;
    }

    public virtual void Update(Camera2D camera, float delta)
    {
        if (timer == 0f)
        {
            return;
        }

        timer -= delta;
        if (timer < 0f)
        {
            timer = 0f;
            timerEnded();
        }
    }

    protected virtual void timerEnded() { }

    public virtual void Start()
    {
        timer = duration;
    }
}