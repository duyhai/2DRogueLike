using Godot;

public partial class PlayerInputController : InputController
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
            player.Shoot(player.ViewDirection);
        }

        Vector2 newVelocity = new Vector2();
        if (Input.IsActionPressed("move_right"))
        {
            newVelocity.X += 1;
        }

        if (Input.IsActionPressed("move_left"))
        {
            newVelocity.X -= 1;
        }

        if (Input.IsActionPressed("move_down"))
        {
            newVelocity.Y += 1;
        }

        if (Input.IsActionPressed("move_up"))
        {
            newVelocity.Y -= 1;
        }

        if (!player.Sliding)
        {
            player.Velocity = newVelocity.Normalized() * player.Stats.Speed;
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