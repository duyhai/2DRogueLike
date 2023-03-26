using Godot;

public partial class Booster : Block
{
    protected PackedScene powerUpScene;

    public Booster() :
        base(new NullInputController(), new NullPhysicsController(), new NullGraphicsController())
    { }

    public void OnArea2DBodyEntered(Node body)
    {
        if (body.GetType() == typeof(Player))
        {
            PowerUp powerUp = (PowerUp)powerUpScene.Instantiate();
            powerUp.Initiate((GameObject)body, this);
            var method = body.GetType().GetMethod("AddPowerUp");
            method?.Invoke(body, new object[] { powerUp });
            QueueFree();
        }
    }
}
