using Godot;

public abstract class Bullet : KinematicBody2D
{
    public Vector2 velocity { get; protected set; }
    protected int speed;
    protected PhysicsController physicsController;

    public override void _PhysicsProcess(float delta)
    {
        physicsController.Update(this, delta);
    }

    public void OnVisibilityNotifier2DScreenExited()
    {
        QueueFree();
    }

    public void Initiate(float rotation, Vector2 position)
    {
        Position = position;
        velocity = new Vector2(speed, 0).Rotated(rotation);
    }
}