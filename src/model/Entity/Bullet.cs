using Godot;

public abstract class Bullet : GameObject
{
    protected int damage;

    public Bullet(InputController inputController, PhysicsController physicsController, GraphicsController graphicsController) :
        base(inputController, physicsController, graphicsController)
    { }

    public void OnVisibilityNotifier2DScreenExited()
    {
        if (!GetTree().Paused)
        {
            QueueFree();
        }
    }

    public void Initiate(float rotation, Vector2 position, int damage)
    {
        Position = position;
        velocity = new Vector2(speed, 0).Rotated(rotation);
        Rotation = (Mathf.Pi / 2) + velocity.Angle();
        this.damage = damage;
    }

    public virtual void HitTarget(KinematicCollision2D collision)
    {
        var collider = collision.Collider;
        var method = collider.GetType().GetMethod("Hit");
        method?.Invoke(collider, new object[] { damage });
    }

    public override void Hit(int damage) { }
}
