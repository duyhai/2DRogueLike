using Godot;

public class PlayerPhysicsController : PhysicsController 
{
    public override void Update(GameObject gameObject, float delta)
    {
        Player player = (Player)gameObject; 
        var collision = player.MoveAndCollide(player.velocity * delta);
        
        if (collision != null)
        {
            player.velocity = player.velocity.Slide(collision.Normal);
            collision = player.MoveAndCollide(player.velocity * delta);
        }
    }
}