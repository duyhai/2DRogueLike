using Godot;

public class SpeedBoost : Booster
{
    public static PackedScene SceneObject = GD.Load<PackedScene>("res://SpeedBoost.tscn");
    public SpeedBoost()
    {
        powerUpScene = GD.Load<PackedScene>("res://SpeedPowerUp.tscn");
    }
}
