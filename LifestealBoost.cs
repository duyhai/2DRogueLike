using Godot;

public class LifestealBoost : Booster
{
    public static PackedScene SceneObject = GD.Load<PackedScene>("res://LifestealBoost.tscn");
    public LifestealBoost()
    {
        powerUpScene = GD.Load<PackedScene>("res://LifestealPowerUp.tscn");
    }
}