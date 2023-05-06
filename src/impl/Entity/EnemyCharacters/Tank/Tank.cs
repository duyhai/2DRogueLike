using Godot;

public partial class Tank : Enemy
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/Entity/Enemies/Tank.tscn");

    public Tank() :
        base(new PlayerFollowingInputController(), new TankPhysicsController(), new BasicGraphicsController())
    {
        baseStats = new StatsInfo { MaxHealth = 2000, MaxShield = 0, Damage = 200, Speed = 50 };
        health = Stats.MaxHealth;
        CollisionLayer = CollisionLayers.Enemy;
        CollisionMask = CollisionLayers.Player | CollisionLayers.Wall | CollisionLayers.Water;
    }
}
