using Godot;

public class Shooter : GameObject
{
	public static PackedScene SceneObject = (PackedScene)GD.Load("res://Shooter.tscn");
	public Weapon weapon;

	public Shooter():
		base(new ShooterInputController(), new SmoothCollidePhysicsController(), new ShooterGraphicsController())
	{
		this.speed = 200;
		this.health = 50;
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
