using Godot;

public class SmoothCollidePhysicsController : PhysicsController
{
    public override void Update(GameObject gameObject, float delta)
    {
        var collision = gameObject.MoveAndCollide(gameObject.velocity * delta);

        if (collision != null)
        {
            gameObject.velocity = gameObject.velocity.Slide(collision.Normal);
            collision = gameObject.MoveAndCollide(gameObject.velocity * delta);
        }
    }
}