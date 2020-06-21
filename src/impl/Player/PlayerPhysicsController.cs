using Godot;

public class PlayerPhysicsController : PhysicsController 
{
    public override void Update(KinematicBody2D node, float delta)
    {
        Player player = (Player)node; 
        var collision = player.MoveAndCollide(player.velocity * delta);
        
        if (collision != null)
        {
            player.velocity = player.velocity.Slide(collision.Normal);
            collision = player.MoveAndCollide(player.velocity * delta);
        }
    }
}