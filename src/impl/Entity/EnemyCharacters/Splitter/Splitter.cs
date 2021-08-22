using Godot;

public class Splitter : Enemy
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/Entity/Enemies/Splitter.tscn");

    public Splitter() :
        base(new NullInputController(), new SmoothCollidePhysicsController(), new BasicGraphicsController())
    {
        baseStats = new StatsInfo { MaxHealth = 40, Damage = 0, Speed = 200 };
        health = Stats.MaxHealth;
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

    public override void PlayDeathAnimation()
    {
        Death();
    }
}
