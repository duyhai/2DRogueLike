using Godot;

public class LifestealBoost : Booster
{
    public static PackedScene SceneObject = GD.Load<PackedScene>("res://scenes/Map/Blocks/Boosters/LifestealBoost.tscn");
    public LifestealBoost()
    {
        powerUpScene = GD.Load<PackedScene>("res://scenes/powerups/LifestealPowerUp.tscn");
    }
}