using Godot;

public class Floater : GameObject
{
	public static PackedScene SceneObject = (PackedScene)GD.Load("res://Floater.tscn");

	public Floater():
		base(new FloaterInputController(), new FloaterPhysicsController(), new FloaterGraphicsController())
	{
		this.speed = 200;
	}

	public override void _Ready()
	{
	}
}