using Godot;

public abstract class Weapon : Node2D
{
    protected int bulletSpeed;
    public float weaponCooldown;
    protected Timer bulletTimer;
    protected PackedScene bulletScene;
    protected WeaponGraphicsController graphicsController;

    public Weapon(WeaponGraphicsController graphicsController)
    {
        this.graphicsController = graphicsController;
    }

    public override void _Process(float delta)
    {
        graphicsController?.Update(this);
    }

    public virtual bool Shoot(Vector2 vector, uint collisionLayer, uint collisionMask)
    {
        if (!bulletTimer.IsStopped())
        {
            return false;
        }

        bulletTimer.Start();
        var bullet = (Bullet)bulletScene.Instance();

        var tip = GetNodeOrNull<Node2D>("Sprite/Tip");

        bullet.Initiate(vector.Angle(), tip != null ? tip.GlobalPosition : ((Node2D)GetParent()).GlobalPosition);
        bullet.CollisionLayer = collisionLayer;
        bullet.CollisionMask = collisionMask;
        GetParent().GetParent().AddChild(bullet);

        return true;
    }

    public void SetWeaponCooldown(float weaponCooldown)
    {
        bulletTimer.WaitTime = weaponCooldown;
    }
}