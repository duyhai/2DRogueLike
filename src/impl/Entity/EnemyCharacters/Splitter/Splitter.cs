using Godot;

public partial class Splitter : Enemy
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/Entity/Enemies/Splitter.tscn");

    public Splitter() :
        base(new NullInputController(), new SmoothCollidePhysicsController(), new BasicGraphicsController())
    {
        baseStats = new StatsInfo { MaxHealth = 400, Damage = 0, Speed = 200 };
        health = Stats.MaxHealth;
        CollisionLayer = CollisionLayers.Enemy;
        CollisionMask = CollisionLayers.Player | CollisionLayers.Wall | CollisionLayers.Water;
    }

    public override void _Ready()
    {
    }

    public override void OnDeathFinished()
    {
        PackedScene floaterScene = Floater.SceneObject;
        for (int i = 0; i < 3; i++)
        {
            var floater = (Floater)floaterScene.Instantiate();
            floater.Position = Position;
            GetParent().AddChild(floater);
        }

        base.OnDeathFinished();
    }
}
