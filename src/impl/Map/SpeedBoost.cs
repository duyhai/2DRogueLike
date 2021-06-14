using Godot;

public class SpeedBoost : Node2D
{
    public void OnArea2DBodyEntered(Node body)
    {
        SpeedPowerUp speedPowerUp = (SpeedPowerUp)GD.Load<PackedScene>("res://SpeedPowerUp.tscn").Instance();
        var method = body.GetType().GetMethod("AddPowerUp");
        method?.Invoke(body, new object[] { speedPowerUp });
    }
}
