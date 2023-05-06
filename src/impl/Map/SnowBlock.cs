using Godot;

public partial class SnowBlock : Block
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/Map/Blocks/SnowBlock.tscn");
    private static PackedScene slidingPowerUpScene = (PackedScene)GD.Load("res://scenes/powerups/SlidingPowerUp.tscn");
    Area2D area;

    public SnowBlock() :
        base(new NullInputController(), new NullPhysicsController(), new NullGraphicsController())
    {

    }

    public override void _Ready()
    {
        CollisionMask = CollisionLayers.Player | CollisionLayers.Enemy;
        area = GetNode<Area2D>("Area2D");
        area.CollisionLayer = CollisionLayer;
        area.CollisionMask = CollisionMask;
    }

    public override void _Process(double delta)
    {
        foreach (var body in area.GetOverlappingBodies())
        {
            if (body is GameObject gameObject)
            {
                SlidingPowerUp powerUp = (SlidingPowerUp)slidingPowerUpScene.Instantiate();
                powerUp.Initiate(gameObject, this);
                var method = body.GetType().GetMethod("AddPowerUp");
                method?.Invoke(body, new object[] { powerUp });
            }
        }
    }
}
