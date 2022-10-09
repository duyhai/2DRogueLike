using Godot;

public class Floater : Enemy
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/Entity/Enemies/Floater.tscn");

    public Floater() :
        base(new FloaterInputController(), new SmoothCollidePhysicsController(), new FloaterGraphicsController())
    {
        baseStats = new StatsInfo { MaxHealth = 300, MaxShield = 0, Damage = 0, Speed = 200 };
        health = Stats.MaxHealth;
        CollisionLayer = CollisionLayers.Enemy;
        CollisionMask = CollisionLayers.Player | CollisionLayers.Wall | CollisionLayers.Water;
    }

    public override void _Ready()
    {
    }
}
