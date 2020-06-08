using Godot;

public class PlayerPhysicsController : PhysicsController 
{
    public override void Update(Player player, float delta)
    {
        var collision = player.MoveAndCollide(player.velocity * delta);
        if (collision != null)
        {
            player.velocity = player.velocity.Slide(collision.Normal);
        }
    }
}