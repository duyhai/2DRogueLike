using Godot;
using System;

public class UnbreakableWall : Block
{
	public UnbreakableWall() :
        base(new NullInputController(), new NullPhysicsController(), new NullGraphicsController()) 
    {
        
    }

    public override void Hit(int damage)
    {

    }
}
