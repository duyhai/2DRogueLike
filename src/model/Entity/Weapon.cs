using Godot;

public abstract class Weapon : Node2D
{
    protected int bulletSpeed;
    public float weaponCooldown;
    protected int damage;
    protected Timer bulletTimer;
    protected PackedScene bulletScene;
    protected WeaponGraphicsController graphicsController;

    public Weapon(WeaponGraphicsController graphicsController, int damage)
    {
        this.graphicsController = graphicsController;
        this.damage = damage;
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

        int modifiedDamage = (int)(GetParent<GameObject>().damageModifier * damage);
        bullet.Initiate(vector.Angle(), tip != null ? tip.GlobalPosition : ((Node2D)GetParent()).GlobalPosition, modifiedDamage);
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