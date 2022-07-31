using Godot;

public class WaterBlock : Block
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/Map/Blocks/WaterBlock.tscn");
    public WaterBlock() :
        base(new NullInputController(), new NullPhysicsController(), new NullGraphicsController())
    {

    }

    public override void _Ready()
    {
        CollisionLayer = CollisionLayers.Water;
    }

    public override int Hit(int damage)
    {
        return 0;
    }
}
