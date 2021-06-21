using Godot;

public class Booster : Block
{
    protected PackedScene powerUpScene;

    public Booster() :
        base(new NullInputController(), new NullPhysicsController(), new NullGraphicsController())
    { }

    public void OnArea2DBodyEntered(Node body)
    {
        if (body.GetType() == typeof(Player))
        {
            var powerUp = powerUpScene.Instance();
            var method = body.GetType().GetMethod("AddPowerUp");
            method?.Invoke(body, new object[] { powerUp });
            QueueFree();
        }
    }
}
