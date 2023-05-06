using Godot;

public partial class BreakableWall : Block
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/Map/Blocks/BreakableWall.tscn");
    public BreakableWall() :
        base(new NullInputController(), new NullPhysicsController(), new NullGraphicsController())
    {

    }

    public override int Hit(int damage)
    {
        QueueFree();
        return 0;
    }
}
