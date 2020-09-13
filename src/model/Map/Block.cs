using Godot;

public abstract class Block : StaticBody2D
{
    public override void _Ready()
    {
		  CollisionLayer = CollisionLayers.MapObject;
    }
    public virtual void Hit() {}
}