using Godot;

public class SpeedBoost : Booster
{
    public static PackedScene SceneObject = GD.Load<PackedScene>("res://scenes/Map/Blocks/Boosters/SpeedBoost.tscn");
    public SpeedBoost()
    {
        powerUpScene = GD.Load<PackedScene>("res://scenes/powerups/SpeedPowerUp.tscn");
    }
}
