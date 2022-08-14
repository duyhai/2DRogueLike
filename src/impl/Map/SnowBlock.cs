using Godot;

public class SnowBlock : Block
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
        CollisionLayer = CollisionLayers.Player | CollisionLayers.Enemy;
        area = GetNode<Area2D>("Area2D");
        area.CollisionLayer = CollisionLayer;
    }

    public override void _Process(float delta)
    {
        foreach (var body in area.GetOverlappingBodies())
        {
            if (body is GameObject)
            {
                GameObject gameObject = (GameObject)body;
                PowerUp powerUp = (PowerUp)slidingPowerUpScene.Instance();
                powerUp.Initiate((GameObject)body, this);
                var method = body.GetType().GetMethod("AddPowerUp");
                method?.Invoke(body, new object[] { powerUp });
            }
        }
    }
}
