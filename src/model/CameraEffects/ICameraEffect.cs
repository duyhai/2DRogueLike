using Godot;

public interface ICameraEffect
{
    bool IsEnded
    {
        get;
    }

    void Update(Camera2D camera, double delta);
    void Start(Camera2D camera);
}