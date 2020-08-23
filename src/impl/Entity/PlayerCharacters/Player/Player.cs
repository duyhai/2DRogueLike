using Godot;

public class Player : GameObject
{
	Weapon weapon;

	public Player() :
		base(new PlayerInputController(), new PlayerPhysicsController(), new PlayerGraphicsController())
	{
		this.speed = 200;
		this.health = 100;
	}

	public override void _Ready()
	{
		weapon = GetNode<Weapon>("Weapon");
	}

	public void Shoot(Vector2 vector)
	{
		weapon.Shoot(vector);
	}
}
