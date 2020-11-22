using Godot;

public class BreakableWall : Block
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://BreakableWall.tscn");
    public BreakableWall() :
        base(new NullInputController(), new NullPhysicsController(), new NullGraphicsController())
    {

    }

    public override void Hit(int damage)
    {
        QueueFree();
    }
}
