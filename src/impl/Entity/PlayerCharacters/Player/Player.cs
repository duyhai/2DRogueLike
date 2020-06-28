using Godot;

public class Player : GameObject
{
	Weapon weapon;

	public Player():
		base(new PlayerInputController(), new PlayerPhysicsController(), new NullGraphicsController())
	{
		this.speed = 200;
	}

	public override void _Ready()
	{
		weapon = GetNode<Weapon>("Weapon");
	}

	public override void _Process(float delta)
	{
		inputController.Update(this);
	}

	public override void _PhysicsProcess(float delta)
	{
		physicsController.Update(this, delta);
	}

	public void Shoot(Vector2 vector)
	{
		weapon.Shoot(vector);
	}
}
