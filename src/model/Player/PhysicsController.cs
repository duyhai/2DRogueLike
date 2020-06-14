using Godot;

public abstract class PhysicsController 
{
    public abstract void Update(KinematicBody2D node, float delta);
}