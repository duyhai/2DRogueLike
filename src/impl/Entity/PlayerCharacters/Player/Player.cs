using Godot;

public class Player : GameObject
{
	public Weapon weapon;

	public Player() :
		base(new PlayerInputController(), new SmoothCollidePhysicsController(), new BasicGraphicsController())
	{
		this.speed = 200;
		this.maxHealth = 100;
		this.health = maxHealth;
		CollisionLayer = CollisionLayers.Player;
		CollisionMask = CollisionLayers.Enemy | CollisionLayers.MapObject;
	}

	public override void _Ready()
	{
		weapon = GetNode<SimpleWeapon>("SimpleWeapon");
		weapon.SetWeaponCooldown(0.25f);
	}

	public void Shoot(Vector2 vector, uint collisionLayer, uint collisionMask)
	{
		weapon.Shoot(vector, collisionLayer, collisionMask);
	}
}
