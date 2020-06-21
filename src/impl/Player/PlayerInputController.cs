using Godot;

public class PlayerInputController : InputController 
{
    public override void Update(Player player)
    {
        if (Input.IsActionPressed("shoot"))
        {
            var bulletDirection = player.GetGlobalMousePosition() - player.Position; 
            player.Shoot(bulletDirection);
        }

        player.velocity = new Vector2();

        if (Input.IsActionPressed("ui_right"))
        {
            player.velocity.x += 1;
        }

        if (Input.IsActionPressed("ui_left"))
        {
            player.velocity.x -= 1;
        }

        if (Input.IsActionPressed("ui_down"))
        {
            player.velocity.y += 1;
        }

        if (Input.IsActionPressed("ui_up"))
        {
            player.velocity.y -= 1;
        }

        player.velocity = player.velocity.Normalized() * player.speed;
    }
}