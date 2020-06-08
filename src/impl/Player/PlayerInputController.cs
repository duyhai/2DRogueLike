using Godot;

public class PlayerInputController : InputController 
{
    public override void Update(Player player)
    {
        player.velocity = new Vector2();

        if (Input.IsActionPressed("ui_right"))
            player.velocity.x += player.speed;

        if (Input.IsActionPressed("ui_left"))
            player.velocity.x -= player.speed;

        if (Input.IsActionPressed("ui_down"))
            player.velocity.y += player.speed;

        if (Input.IsActionPressed("ui_up"))
            player.velocity.y -= player.speed;
    }
}