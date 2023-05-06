using Godot;
using System;

public partial class UnbreakableWall : Block
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/Map/Blocks/WallBlock.tscn");
    public UnbreakableWall() :
        base(new NullInputController(), new NullPhysicsController(), new NullGraphicsController())
    {

    }

    public override int Hit(int damage)
    {
        return 0;
    }
}
