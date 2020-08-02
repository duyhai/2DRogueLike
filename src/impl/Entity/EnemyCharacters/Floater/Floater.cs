using Godot;

public class Floater : GameObject
{
	Weapon weapon;

	public Floater():
		base(new FloaterInputController(), new FloaterPhysicsController(), new FloaterGraphicsController())
	{
		this.speed = 200;
	}

	public override void _Ready()
	{
	}
}
