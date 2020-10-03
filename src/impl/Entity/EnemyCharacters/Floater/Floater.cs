using Godot;

public class Floater : Enemy
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://Floater.tscn");

    public Floater() :
        base(new FloaterInputController(), new SmoothCollidePhysicsController(), new BasicGraphicsController())
    {
        this.speed = 200;
        this.health = 30;
        this.health = maxHealth;
        CollisionLayer = CollisionLayers.Enemy;
        CollisionMask = CollisionLayers.Player | CollisionLayers.MapObject;
    }

    public override void _Ready()
    {
    }
}
