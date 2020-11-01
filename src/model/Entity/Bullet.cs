using Godot;

public abstract class Bullet : GameObject
{
    public int damage;

    public Bullet(InputController inputController, PhysicsController physicsController, GraphicsController graphicsController, int damage) :
        base(inputController, physicsController, graphicsController)
    {
        this.damage = damage;
    }

    public void OnVisibilityNotifier2DScreenExited()
    {
        if (!GetTree().Paused)
        {
            QueueFree();
        }
    }

    public void Initiate(float rotation, Vector2 position)
    {
        Position = position;
        velocity = new Vector2(speed, 0).Rotated(rotation);
    }

    public virtual void HitTarget(KinematicCollision2D collision)
    {
        var collider = collision.Collider;
        var method = collider.GetType().GetMethod("Hit");
        method?.Invoke(collider, new object[] { damage });
        //QueueFree();
    }

    public override void Hit(int damage) { }
}