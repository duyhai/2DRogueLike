using Godot;

abstract public partial class CameraController
{
    protected Camera2D camera;

    public CameraController(Camera2D camera)
    {
        this.camera = camera;
    }

    abstract public void Update(GameObject gameObject, double delta);
}