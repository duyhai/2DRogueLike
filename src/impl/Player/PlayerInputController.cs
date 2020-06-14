using Godot;

public class PlayerInputController : InputController 
{
    public override void Update(Player player)
    {
        player.velocity = new Vector2();

        if (Input.IsActionPressed("ui_right"))
        {
            player.velocity.x += 1;
            player.lastOrientation = player.velocity;
        }

        if (Input.IsActionPressed("ui_left"))
        {
            player.velocity.x -= 1;
            player.lastOrientation = player.velocity;
        }

        if (Input.IsActionPressed("ui_down"))
        {
            player.velocity.y += 1;
            player.lastOrientation = player.velocity;
        }

        if (Input.IsActionPressed("ui_up"))
        {
            player.velocity.y -= 1;
            player.lastOrientation = player.velocity;
        }

        player.velocity = player.velocity.Normalized() * player.speed;

        if (Input.IsActionPressed("shoot"))
            player.Shoot(player.lastOrientation);
    }
}