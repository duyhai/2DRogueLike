using Godot;

public class SmoothCollidePhysicsController : PhysicsController
{
    public override void Update(GameObject gameObject, float delta)
    {
        var modifiedVelocity = gameObject.velocity;
        var collision = gameObject.MoveAndCollide(modifiedVelocity * delta);

        if (collision != null)
        {
            modifiedVelocity = modifiedVelocity.Slide(collision.Normal);
            collision = gameObject.MoveAndCollide(modifiedVelocity * delta);
        }
    }
}