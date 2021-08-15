using Godot;

public class Floater : Enemy
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/Entity/Enemies/Floater.tscn");

    public Floater() :
        base(new FloaterInputController(), new SmoothCollidePhysicsController(), new BasicGraphicsController())
    {
        baseStats = new StatsInfo(30, 0, 200);
        health = Stats.MaxHealth;
        CollisionLayer = CollisionLayers.Enemy;
        CollisionMask = CollisionLayers.Player | CollisionLayers.MapObject;
    }

    public override void _Ready()
    {
    }
}
