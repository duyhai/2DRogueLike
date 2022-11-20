using Godot;

abstract public class CameraController
{
    protected Camera2D camera;

    public CameraController(Camera2D camera)
    {
        this.camera = camera;
    }

    abstract public void Update(GameObject gameObject, float delta);
}