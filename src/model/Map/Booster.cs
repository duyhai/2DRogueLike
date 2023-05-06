using Godot;

public partial class Booster : Block
{
    protected PackedScene powerUpScene;

    public Booster() :
        base(new NullInputController(), new NullPhysicsController(), new NullGraphicsController())
    { }

    public override void _Ready()
    {
        base._Ready();

        var area2D = GetNode<Area2D>("Area2D");
        area2D.CollisionMask = CollisionMask;
    }

    public void OnArea2DBodyEntered(Node2D body)
    {
        GD.Print("Pre condition");
        if (body.GetType() == typeof(Player))
        {
            GD.Print("Booster");
            PowerUp powerUp = (PowerUp)powerUpScene.Instantiate();
            powerUp.Initiate((GameObject)body, this);
            var method = body.GetType().GetMethod("AddPowerUp");
            method?.Invoke(body, new object[] { powerUp });
            QueueFree();
        }
    }
}
