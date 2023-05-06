using Godot;

public partial class SimpleShieldBoost : Booster
{
    public static PackedScene SceneObject = GD.Load<PackedScene>("res://scenes/Map/Blocks/Boosters/SimpleShieldBoost.tscn");
    public SimpleShieldBoost()
    {
        powerUpScene = GD.Load<PackedScene>("res://scenes/powerups/SimpleShieldPowerUp.tscn");
    }
}
