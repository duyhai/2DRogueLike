using Godot;

public abstract partial class Bullet : GameObject
{
    protected int damage;
    protected GameObject initiator;
    public GameObject Initiator
    {
        get
        {
            return initiator;
        }
    }

    public Bullet(InputController inputController, PhysicsController physicsController, GraphicsController graphicsController) :
        base(inputController, physicsController, graphicsController)
    { }

    public override void _Ready()
    {
        AddToGroup(NodeGroups.Bullet);
    }

    public void OnVisibilityNotifier2DScreenExited()
    {
        if (!GetTree().Paused)
        {
            QueueFree();
        }
    }

    public virtual void Initiate(GameObject initiator, float rotation, Vector2 position, int damage)
    {
        Position = position;
        Velocity = new Vector2(Stats.Speed, 0).Rotated(rotation);
        Rotation = (Mathf.Pi / 2) + rotation;
        this.damage = damage;
        this.initiator = initiator;
    }

    public virtual int HitTarget(Node collider)
    {
        return DamageUtil.HandleDamage(initiator, collider, damage);
    }

    public override int Hit(int damage) { return 0; }
}
