using Godot;

public class Tank : Enemy
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/Entity/Enemies/Tank.tscn");

    public Tank() :
        base(new TankInputController(), new TankPhysicsController(), new BasicGraphicsController())
    {
        baseStats = new StatsInfo { MaxHealth = 2000, Damage = 200, Speed = 50 };
        health = Stats.MaxHealth;
        CollisionLayer = CollisionLayers.Enemy;
        CollisionMask = CollisionLayers.Player | CollisionLayers.MapObject;
    }
}
