using Godot;

public interface ICameraEffect
{
    bool IsEnded
    {
        get;
    }

    void Update(Camera2D camera, float delta);
    void Start(Camera2D camera);
}