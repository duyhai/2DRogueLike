using Godot;

public partial class Shooter : Enemy
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/Entity/Enemies/Shooter.tscn");
    public Weapon weapon;

    public Shooter() :
        base(new ShooterInputController(), new SmoothCollidePhysicsController(), new BasicGraphicsController())
    {
        baseStats = new StatsInfo { MaxHealth = 500, MaxShield = 0, Damage = 200, Speed = 200 };
        health = Stats.MaxHealth;
        CollisionLayer = CollisionLayers.Enemy;
        CollisionMask = CollisionLayers.Player | CollisionLayers.Wall | CollisionLayers.Water;
    }

    public override void _Ready()
    {
        base._Ready();
        weapon = GetNode<ShooterWeapon>("ShooterWeapon");
        weapon.SetWeaponCooldown(1f);
    }

    public void Shoot(Vector2 vector)
    {
        weapon.Shoot(vector);
    }
}
