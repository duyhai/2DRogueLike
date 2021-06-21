using Godot;

public class DamageBoost : Booster
{
    public static PackedScene SceneObject = GD.Load<PackedScene>("res://DamageBoost.tscn");
    public DamageBoost()
    {
        powerUpScene = GD.Load<PackedScene>("res://DamageBoostPowerUp.tscn");
    }
}
