using Godot;

public class Shooter : Enemy
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/Entity/Enemies/Shooter.tscn");
    public Weapon weapon;

    public Shooter() :
        base(new ShooterInputController(), new SmoothCollidePhysicsController(), new BasicGraphicsController())
    {
        baseStats = new StatsInfo { MaxHealth = 500, MaxShield = 0, Damage = 200, Speed = 200 };
        health = Stats.MaxHealth;
        CollisionLayer = CollisionLayers.Enemy;
        CollisionMask = CollisionLayers.Player | CollisionLayers.MapObject;
    }

    public override void _Ready()
    {
        weapon = GetNode<ShooterWeapon>("ShooterWeapon");
        weapon.SetWeaponCooldown(1f);
    }

    public void Shoot(Vector2 vector, uint CollisionLayer, uint CollisionMask)
    {
        weapon.Shoot(vector, CollisionLayer, CollisionMask);
    }
}
