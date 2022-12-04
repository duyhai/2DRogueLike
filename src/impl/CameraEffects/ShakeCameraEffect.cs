using Godot;
using System;

public class ShakeCameraEffect : CameraEffect
{
    int Frequency { get; set; }
    float Amplitude { get; set; }
    float lastShookTimer = 0;
    float periodInMs = 0;
    float previousX = 0;
    float previousY = 0;
    Vector2 lastOffset = new Vector2(0, 0);

    public ShakeCameraEffect(float duration, int frequency, float amplitude) : base(duration)
    {
        this.Frequency = frequency;
        this.Amplitude = amplitude;
        this.periodInMs = 1.0f / this.Frequency;
    }

    public override void Update(Camera2D camera, float delta)
    {
        base.Update(camera, delta);

        if (Timer == 0f)
        {
            return;
        }

        Random rnd = new Random();

        while (lastShookTimer >= periodInMs)
        {
            lastShookTimer -= periodInMs;
            float intensity = Amplitude * (1.0f - ((Duration - Timer) / Duration));
            float newX = (float)(rnd.NextDouble() * 2.0f) - 1.0f;  // -1.0 to 1.0
            float xComponent = intensity * (previousX + (delta * (newX - previousX)));
            float newY = (float)(rnd.NextDouble() * 2.0f) - 1.0f;  // -1.0 to 1.0
            float yComponent = intensity * (previousY + (delta * (newY - previousY)));
            previousX = newX;
            previousY = newY;
            Vector2 newOffset = new Vector2(xComponent, yComponent);
            camera.Offset = camera.Offset - lastOffset + newOffset;
            lastOffset = newOffset;
        }

        lastShookTimer += delta;
    }

    public override void Start()
    {
        Random rnd = new Random();
        base.Start();
        previousX = (float)(rnd.NextDouble() * 2.0f) - 1.0f;  // -1.0 to 1.0
        previousY = (float)(rnd.NextDouble() * 2.0f) - 1.0f;  // -1.0 to 1.0
    }
}