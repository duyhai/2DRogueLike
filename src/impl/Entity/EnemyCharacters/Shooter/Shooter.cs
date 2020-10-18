using Godot;

public class Shooter : Enemy
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://Shooter.tscn");
    public Weapon weapon;

<<<<<<< HEAD
    public Shooter():
=======
    public Shooter() :
>>>>>>> master
        base(new ShooterInputController(), new SmoothCollidePhysicsController(), new BasicGraphicsController())
    {
        this.speed = 200;
        this.health = 50;
<<<<<<< HEAD
        this.health = maxHealth;
=======
        this.maxHealth = health;
>>>>>>> master
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
