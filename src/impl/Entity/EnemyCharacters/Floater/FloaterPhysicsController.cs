using Godot;

public class FloaterPhysicsController : PhysicsController 
{
    public override void Update(GameObject gameObject, float delta)
    {
        Floater floater = (Floater)gameObject; 
        var collision = floater.MoveAndCollide(floater.velocity * delta);
        
        if (collision != null)
        {
            floater.velocity = floater.velocity.Slide(collision.Normal);
            collision = floater.MoveAndCollide(floater.velocity * delta);
        }
    }
}