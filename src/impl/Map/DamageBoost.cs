using Godot;

public class DamageBoost : Node2D
{
    public void OnArea2DBodyEntered(Node body)
    {
        DamageBoostPowerUp damageBoostPowerUp = (DamageBoostPowerUp)GD.Load<PackedScene>("res://DamageBoostPowerUp.tscn").Instance();
        var method = body.GetType().GetMethod("AddPowerUp");
        method?.Invoke(body, new object[] { damageBoostPowerUp });
    }
}
