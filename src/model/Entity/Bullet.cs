using Godot;

public abstract class Bullet : GameObject
{
    public Bullet(InputController inputController, PhysicsController physicsController, GraphicsController graphicsController):
        base(inputController, physicsController, graphicsController)
    {}
    public void OnVisibilityNotifier2DScreenExited()
    {
        if (!GetTree().Paused) {
            QueueFree();
        }
    }

    public void Initiate(float rotation, Vector2 position)
    {
        Position = position;
        velocity = new Vector2(speed, 0).Rotated(rotation);
    }
}