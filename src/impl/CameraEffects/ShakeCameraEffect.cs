using Godot;
using System;

public partial class ShakeCameraEffect : TimedCameraEffect
{
    int Frequency { get; set; }
    double Amplitude { get; set; }
    double lastShookTimer = 0;
    double periodInMs = 0;
    double previousX = 0;
    double previousY = 0;
    Vector2 lastOffset = new Vector2(0, 0);

    public ShakeCameraEffect(float duration, int frequency, float amplitude) : base(duration)
    {
        this.Frequency = frequency;
        this.Amplitude = amplitude;
        this.periodInMs = 1.0f / this.Frequency;
    }

    public override void Update(Camera2D camera, double delta)
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
            double intensity = Amplitude * (1.0f - ((Duration - Timer) / Duration));
            double newX = (rnd.NextDouble() * 2.0f) - 1.0f;  // -1.0 to 1.0
            double xComponent = intensity * (previousX + (delta * (newX - previousX)));
            double newY = (rnd.NextDouble() * 2.0f) - 1.0f;  // -1.0 to 1.0
            double yComponent = intensity * (previousY + (delta * (newY - previousY)));
            previousX = newX;
            previousY = newY;
            Vector2 newOffset = new Vector2((float)xComponent, (float)yComponent);
            camera.Offset = camera.Offset - lastOffset + newOffset;
            lastOffset = newOffset;
        }

        lastShookTimer += delta;
    }

    public override void Start(Camera2D camera)
    {
        base.Start(camera);
        Random rnd = new Random();
        previousX = (rnd.NextDouble() * 2.0f) - 1.0f;  // -1.0 to 1.0
        previousY = (rnd.NextDouble() * 2.0f) - 1.0f;  // -1.0 to 1.0
    }
}