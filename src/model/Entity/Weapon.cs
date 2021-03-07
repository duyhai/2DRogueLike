using Godot;

public abstract class Weapon : Node2D
{
    protected int bulletSpeed;
    public float weaponCooldown;
    protected Timer bulletTimer;
    protected PackedScene bulletScene;
    WeaponGraphicsController graphicsController;

    public Weapon(WeaponGraphicsController graphicsController)
    {
        this.graphicsController = graphicsController;
    }

    public override void _Process(float delta)
    {
        graphicsController?.Update(this);
    }

    public virtual void Shoot(Vector2 vector, uint collisionLayer, uint collisionMask)
    {
        if (bulletTimer.IsStopped())
        {
            bulletTimer.Start();
            var bullet = (Bullet)bulletScene.Instance();

            var tip = GetNodeOrNull<Node2D>("Tip");

            bullet.Initiate(vector.Angle(), tip != null ? tip.GlobalPosition : ((Node2D)GetParent()).GlobalPosition);
            bullet.CollisionLayer = collisionLayer;
            bullet.CollisionMask = collisionMask;
            GetParent().GetParent().AddChild(bullet);
        }
    }

    public void SetWeaponCooldown(float weaponCooldown)
    {
        bulletTimer.WaitTime = weaponCooldown;
    }
}