using Godot;

public partial class DamageBoost : Booster
{
    public static PackedScene SceneObject = GD.Load<PackedScene>("res://scenes/Map/Blocks/Boosters/DamageBoost.tscn");
    public DamageBoost()
    {
        powerUpScene = GD.Load<PackedScene>("res://scenes/powerups/DamageBoostPowerUp.tscn");
    }
}
