using Godot;

public class Shooter : Enemy
{
	public static PackedScene SceneObject = (PackedScene)GD.Load("res://Shooter.tscn");
	public Weapon weapon;

	public Shooter():
		base(new ShooterInputController(), new SmoothCollidePhysicsController(), new BasicGraphicsController())
	{
		this.speed = 200;
		this.health = 50;
		this.health = maxHealth;
		CollisionLayer = CollisionLayers.Enemy;
		CollisionMask = CollisionLayers.Player | CollisionLayers.MapObject;
	}

	public override void _Ready()
	{
		weapon = GetNode<SimpleWeapon>("SimpleWeapon");
		weapon.SetWeaponCooldown(1f);
	}

	public void Shoot(Vector2 vector, uint CollisionLayer, uint CollisionMask)
	{
		weapon.Shoot(vector, CollisionLayer, CollisionMask);
	}
}
