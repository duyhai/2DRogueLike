using Godot;

public class PlayerInputController : InputController
{
    public override void Update(GameObject gameObject)
    {
        Player player = (Player)gameObject;

        if (Input.IsActionPressed("menu"))
        {
            SceneManager.Instance.GotoScene("res://scenes/MenuScenes/MainMenu.tscn", true);
        }

        if (Input.IsActionPressed("shoot"))
        {
            var bulletDirection = player.GetGlobalMousePosition() - player.weapons[player.equippedWeaponIndex].GlobalPosition;
            player.Shoot(bulletDirection);
        }

        Vector2 newVelocity = new Vector2();
        if (Input.IsActionPressed("ui_right"))
        {
            newVelocity.x += 1;
        }

        if (Input.IsActionPressed("ui_left"))
        {
            newVelocity.x -= 1;
        }

        if (Input.IsActionPressed("ui_down"))
        {
            newVelocity.y += 1;
        }

        if (Input.IsActionPressed("ui_up"))
        {
            newVelocity.y -= 1;
        }

        if (!player.Sliding)
        {
            player.velocity = newVelocity.Normalized() * player.Stats.Speed;
        }

        if (Input.IsActionJustPressed("previous_weapon"))
        {
            player.PreviousWeapon();
        }

        if (Input.IsActionJustPressed("next_weapon"))
        {
            player.NextWeapon();
        }
    }
}