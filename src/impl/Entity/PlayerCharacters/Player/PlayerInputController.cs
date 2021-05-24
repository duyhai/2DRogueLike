using Godot;

public class PlayerInputController : InputController
{
    public override void Update(GameObject gameObject)
    {
        Player player = (Player)gameObject;
        player.velocity = new Vector2();

        if (gameObject.disableInput) return;

        if (Input.IsActionPressed("menu"))
        {
            SceneManager.Instance.GotoScene("res://MenuScenes/MainMenu.tscn", true);
        }

        if (Input.IsActionPressed("shoot"))
        {
            var bulletDirection = player.GetGlobalMousePosition() - player.weapons[player.equippedWeaponIndex].GlobalPosition;
            player.Shoot(bulletDirection, player.CollisionLayer, player.CollisionMask);
        }

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

        if (Input.IsActionJustPressed("previous_weapon"))
        {
            player.weapons[player.equippedWeaponIndex--].Visible = false;
            player.equippedWeaponIndex = (player.equippedWeaponIndex + player.weapons.Count) % player.weapons.Count;
            player.weapons[player.equippedWeaponIndex].Visible = true;
        }

        if (Input.IsActionJustPressed("next_weapon"))
        {
            player.weapons[player.equippedWeaponIndex++].Visible = false;
            player.equippedWeaponIndex %= player.weapons.Count;
            player.weapons[player.equippedWeaponIndex].Visible = true;
        }

        player.velocity = player.velocity.Normalized() * player.speed;
    }
}