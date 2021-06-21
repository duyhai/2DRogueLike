using Godot;
using System;

public class UnbreakableWall : Block
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://WallBlock.tscn");
    public UnbreakableWall() :
        base(new NullInputController(), new NullPhysicsController(), new NullGraphicsController())
    {

    }

    public override int Hit(int damage)
    {
        return 0;
    }
}
