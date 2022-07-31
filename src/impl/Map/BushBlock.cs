using Godot;

public class BushBlock : Block
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/Map/Blocks/BushBlock.tscn");
    public BushBlock() :
        base(new NullInputController(), new NullPhysicsController(), new NullGraphicsController())
    {

    }

    public override void _Ready()
    {
        CollisionLayer = 0;
    }

    public override int Hit(int damage)
    {
        return 0;
    }
}
