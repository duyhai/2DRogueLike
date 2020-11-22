using Godot;

public class Tank : Enemy
{
    public int damage;
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://Tank.tscn");
    Timer attackTimer;

    public Tank() :
        base(new TankInputController(), new TankPhysicsController(), new BasicGraphicsController())
    {
        this.speed = 50;
        this.maxHealth = 200;
        this.health = maxHealth;
        this.damage = 20;
        CollisionLayer = CollisionLayers.Enemy;
        CollisionMask = CollisionLayers.Player | CollisionLayers.MapObject;
    }
 
    public override void _Ready()
    {
        attackTimer = GetNode<Timer>("Timer");
    }

    public virtual void HitTarget(KinematicCollision2D collision)
    {
        if (attackTimer.IsStopped())
        {
            attackTimer.Start();
            var collider = collision.Collider;
            var method = collider.GetType().GetMethod("Hit");
            method?.Invoke(collider, new object[] { damage });
        }
    }
}
