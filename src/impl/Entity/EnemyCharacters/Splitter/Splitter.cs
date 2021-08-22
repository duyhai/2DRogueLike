using Godot;

public class Splitter : Enemy
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/Entity/Enemies/Splitter.tscn");

    public Splitter() :
        base(new NullInputController(), new SmoothCollidePhysicsController(), new BasicGraphicsController())
    {
        this.baseSpeed = 200;
        this.maxHealth = 40;
        this.health = maxHealth;
        CollisionLayer = CollisionLayers.Enemy;
        CollisionMask = CollisionLayers.Player | CollisionLayers.MapObject;
    }

    public override void _Ready()
    {
    }

    public override void Death()
    {
        PackedScene floaterScene = Floater.SceneObject;
        for (int i = 0; i < 3; i++)
        {
            var floater = (Floater)floaterScene.Instance();
            floater.Position = Position;
            GetParent().AddChild(floater);
        }

        base.Death();
    }
}
