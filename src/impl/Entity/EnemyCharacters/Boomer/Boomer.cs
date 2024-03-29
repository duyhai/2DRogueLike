using Godot;

public partial class Boomer : Enemy
{
    // TODO: scene
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/Entity/Enemies/Boomer.tscn");

    public Boomer() :
        base(new PlayerFollowingInputController(), new BoomerPhysicsController(), new BasicGraphicsController())
    {
        baseStats = new StatsInfo { MaxHealth = 700, MaxShield = 0, Damage = 200, Speed = 50 };
        health = Stats.MaxHealth;
        CollisionLayer = CollisionLayers.Enemy;
        CollisionMask = CollisionLayers.Player | CollisionLayers.Wall | CollisionLayers.Water;

        // Instead of playing the death animation,
        // the entity explodes immidiately on death
        Connect("DeathSignal", new Callable(this, nameof(OnDeathFinished)));
    }

    public override void OnDeathFinished()
    {
        Explosion();
    }

    private void Explosion()
    {
        SoundManager.Instance.PlaySound(SoundPaths.Explosion, Position);

        RocketExplosion rocketExplosion = (RocketExplosion)RocketExplosion.SceneObject.Instantiate();
        rocketExplosion.Initiate(this, Position, Stats.Damage);
        rocketExplosion.CollisionLayer = CollisionLayer;
        rocketExplosion.CollisionMask = CollisionMask;
        GetParent().AddChild(rocketExplosion);

        Velocity = Vector2.Zero;
        QueueFree();
    }
}
