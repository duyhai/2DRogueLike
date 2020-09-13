using Godot;

public abstract class Bullet : GameObject
{
    public int damage;

    public Bullet(InputController inputController, PhysicsController physicsController, GraphicsController graphicsController, int damage):
        base(inputController, physicsController, graphicsController)
    {
        this.damage = damage;
    }

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

    public void HitTarget(KinematicCollision2D collision)
    {
        var collider = collision.Collider;
        var method = collider.GetType().GetMethod("Hit");
        if (method != null)
        {
            method.Invoke(collider, new object[]{ damage });
        }
        // if (collider.GetType().ToString() == "BreakableWall")
        // {
        //     var breakableWall = (BreakableWall)collider;
        //     breakableWall.Hit();
        // }
        // else if (collider.GetType().BaseType.BaseType.ToString() == "GameObject")
        // {
            
        //     if ()
        //     var gameObject = (GameObject)collider;
        //     gameObject.Hit(damage);
        // }
        QueueFree();
    }
}